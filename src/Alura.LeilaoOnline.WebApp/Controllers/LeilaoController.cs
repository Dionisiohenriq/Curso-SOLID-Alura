using System;
using Microsoft.AspNetCore.Mvc;
using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Dados.Interfaces;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    public class LeilaoController : Controller
    {  
        ILeilaoDao _dao;
        public LeilaoController(ILeilaoDao leilaoDao)
        {
            _dao = leilaoDao;
        }

        public IActionResult Index()
        {
            return View(_dao.GetAllLeiloes());
        } 

        [HttpGet]
        public IActionResult Insert()
        {
            ViewData["Categorias"] = _dao.GetAllCategorias();
            ViewData["Operacao"] = "Inclusão";

            return View("Form");
        }

        [HttpPost]
        public IActionResult Insert(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _dao.Insert(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _dao.GetAllCategorias();
            ViewData["Operacao"] = "Inclusão";

            return View("Form", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["Categorias"] = _dao.GetAllCategorias();
            ViewData["Operacao"] = "Edição";

            Leilao leilao = _dao.FindLeilaoById(id);
            
            if (leilao == null) return NotFound();

            return View("Form", leilao);
        }

        [HttpPost]
        public IActionResult Edit(Leilao model)
        {
            if (ModelState.IsValid)
            {
                _dao.Update(model);
                return RedirectToAction("Index");
            }

            ViewData["Categorias"] = _dao.GetAllCategorias();
            ViewData["Operacao"] = "Edição";
            
            return View("Form", model);
        }

        [HttpPost]
        public IActionResult Inicia(int id)
        {
            Leilao leilao = _dao.FindLeilaoById(id);
            
            if (leilao == null) return NotFound();
            if (leilao.Situacao != SituacaoLeilao.Rascunho) return StatusCode(405);
            
            leilao.Situacao = SituacaoLeilao.Pregao;
            leilao.Inicio = DateTime.Now;
            
            _dao.Update(leilao);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Finaliza(int id)
        {
            var leilao = _dao.FindLeilaoById(id);
            
            if (leilao == null) return NotFound();
            if (leilao.Situacao != SituacaoLeilao.Pregao) return StatusCode(405);
            
            leilao.Situacao = SituacaoLeilao.Finalizado;
            leilao.Termino = DateTime.Now;
            
            _dao.Update(leilao);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var leilao = _dao.FindLeilaoById(id);
            
            if (leilao == null) return NotFound();
            if (leilao.Situacao == SituacaoLeilao.Pregao) return StatusCode(405);
            
            _dao.Update(leilao);
            
            return NoContent();
        }

        [HttpGet]
        public IActionResult Pesquisa(string termo)
        {
            ViewData["termo"] = termo;

            return View("Index", _dao.SearchLeiloes(termo));
        }
    }
}
