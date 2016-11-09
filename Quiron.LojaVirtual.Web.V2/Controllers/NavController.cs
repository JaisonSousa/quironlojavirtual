﻿using Quiron.LojaVirtual.Dominio.Repositorio;
using Quiron.LojaVirtual.Web.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiron.LojaVirtual.Web.V2.Controllers
{
    public class NavController : Controller
    {
        private ProdutoModeloRepositorio _repositorio;
        private ProdutosViewModel _model;

        //
        // GET: /Nav/
        public ActionResult Index()
        {
            _repositorio = new ProdutoModeloRepositorio();

            var produtos = _repositorio.ObterProdutosVitrine();
            _model = new ProdutosViewModel { Produtos = produtos };

            

            return View(_model);
        }

        public JsonResult TesteMetedoVetrine()
        {
            ProdutoModeloRepositorio repositorio = new ProdutoModeloRepositorio();
            var produtos = repositorio.ObterProdutosVitrine(categoria:"0003",marca:"0002");

            return Json(produtos, JsonRequestBehavior.AllowGet);

 

        }

        [Route("nav/{id}/{marca}")]
        public ActionResult ObterProdutosPorMarcas(string id, string marca)
        {
            //var _model = new ProdutosViewModel { Produtos = null };

            //return View("Index", _model);

            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(marca: id);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = marca };
            return View("Navegacao", _model);
                 
        }

        [Route("nav/{times}/{id}/{clube}")]
        public ActionResult ObterProdutosPorClubes(string id, string clube)
        {
            //var _model = new ProdutosViewModel { Produtos = null };

            //return View("Index", _model);

            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(linha: id);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = clube };
            return View("Navegacao", _model);

        }

        [Route("nav/{genero}/{id}/{genero}")]
        public ActionResult ObterProdutosPorGenero(string id, string genero)
        {
            //var _model = new ProdutosViewModel { Produtos = null };

            //return View("Index", _model);

            _repositorio = new ProdutoModeloRepositorio();
            var produtos = _repositorio.ObterProdutosVitrine(genero: id);
            _model = new ProdutosViewModel { Produtos = produtos, Titulo = genero };
            return View("Navegacao", _model);

        }
	}
}