﻿using Quiron.LojaVirtual.Dominio.Entidades;
using Quiron.LojaVirtual.Dominio.Repositorio;
using Quiron.LojaVirtual.Web.Models;
using Quiron.LojaVirtual.Web.V2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiron.LojaVirtual.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private ProdutosRepositorio _repositorio = new ProdutosRepositorio();

        //Aula 48 sem sessão
        public ViewResult Index(Carrinho carrinho,string returnUrl)
        {
            return View(new CarrinhoViewModel
            {
                Carrinho = carrinho,
                ReturnUrl = returnUrl
            });


        }

        //Aula 48 sem sessão
        public ViewResult FecharPedido()
        {
            return View(new Pedido());

        }

      
        //Aula 48
        [HttpPost]
        public ViewResult FecharPedido(Carrinho carrinho,Pedido pedido)
        {
            
            EmailConfiguracoes email = new EmailConfiguracoes
            {
                EscreverArquivo = bool.Parse(ConfigurationManager.AppSettings["Email.EscreverArquivo"] ?? "falso")
            };
            EmailPedido emailPedido = new EmailPedido(email);

            if (!carrinho.ItensCarrinhos.Any())
            {
                ModelState.AddModelError("", "Não foi possivel concluir o pedido, seu carrinho está vazio!");

            }

            if (ModelState.IsValid)
            {
                emailPedido.ProcessarPedido(carrinho, pedido);
                carrinho.LimpaCarrinho();
                return View("PedidoConcluido");
            }
            else
            {
                return View(pedido);

            }


        }

        //Aula 48 sem sessão
        public PartialViewResult Resumo(Carrinho carrinho)
        {

            return PartialView(carrinho);
        }

        //Aula 48 sem sessão
        //
        // GET: /Carrinho/
        public RedirectToRouteResult Adicionar(Carrinho carrinho,int produtoId,int quantidade, string returnUrl)
        {
            _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto != null)
            {

                carrinho.AdicionarItem(produto, quantidade);

            }

            return RedirectToAction("Index", new { returnUrl });
        }

        //Aula 48 sem sessão
        public RedirectToRouteResult Remover(Carrinho carrinho,int produtoId, string returnUrl)
        {
            _repositorio = new ProdutosRepositorio();

            Produto produto = _repositorio.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produto != null)
            {
                carrinho.RemoverItemCarrinho(produto);
            }

            return RedirectToAction("Index", new { returnUrl });

        }



        ////
        //// GET: /Carrinho/
        //public RedirectToRouteResult Adicionar(int produtoId, string returnUrl)
        //{
        //    _repositorio = new ProdutosRepositorio();

        //    Produto produto = _repositorio.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

        //    if (produto != null)
        //    {

        //        ObterCarrinho().AdicionarItem(produto, 1);
                
        //    }

        //    return RedirectToAction("Index", new {returnUrl });
        //}

        //Sesão aula 47
        //private Carrinho ObterCarrinho()
        //{
        //    Carrinho carrinho = (Carrinho)Session["Carrinho"];

        //    if (carrinho == null)
        //    {
        //        carrinho = new Carrinho();
        //        Session["Carrinho"] = carrinho;

        //    }

        //    return carrinho;

        //}

        //public RedirectToRouteResult Remover(int produtoId, string returnUrl)
        //{
        //    _repositorio = new ProdutosRepositorio();

        //    Produto produto = _repositorio.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

        //    if (produto != null)
        //    {
        //        ObterCarrinho().RemoverItemCarrinho(produto);
        //    }

        //    return RedirectToAction("Index", new { returnUrl });

        //}

        //public ViewResult Index(string returnUrl)
        //{
        //    return View(new CarrinhoViewModel
        //        {
        //            Carrinho = ObterCarrinho(),
        //            ReturnUrl = returnUrl
        //        });


        //}

        //public PartialViewResult Resumo()
        //{
        //    Carrinho carrinho = ObterCarrinho();
        //    return PartialView(carrinho);
        //}

        //public ViewResult FecharPedido()
        //{
        //    return View(new Pedido());

        //}

        //[HttpPost]
        //public ViewResult FecharPedido(Pedido pedido)
        //{
        //    Carrinho carrinho = ObterCarrinho();
        //    EmailConfiguracoes email = new EmailConfiguracoes
        //    {
        //        EscreverArquivo = bool.Parse(ConfigurationManager.AppSettings["Email.EscreverArquivo"] ?? "falso")
        //    };
        //        EmailPedido emailPedido = new EmailPedido(email);

        //        if (!carrinho.ItensCarrinhos.Any())
        //        {
        //            ModelState.AddModelError("","Não foi possivel concluir o pedido, seu carrinho está vazio!");
		 
        //        }
                
        //        if(ModelState.IsValid)
        //        {
        //            emailPedido.ProcessarPedido(carrinho, pedido);
        //            carrinho.LimpaCarrinho();
        //            return View("PedidoConcluido");
        //        }
        //        else
        //        {
        //            return View(pedido);

        //        }
            

        //}

        public ViewResult PedidoConcluido()
        {
            return View();
        }

	}
}