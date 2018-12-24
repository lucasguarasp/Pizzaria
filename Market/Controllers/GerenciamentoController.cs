using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class GerenciamentoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GerenciamentoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Categorias()
        {
            ViewBag.Categorias = _db.Categorias.ToList();

            if (ViewBag.Categorias != null)
            {
                return View(ViewBag.Categorias);
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult AddCategoria(Categoria cat)
        {
            var categoria = new Categoria
            {
                Descricao = cat.Descricao
            };
            _db.Categorias.Add(categoria);
            _db.SaveChanges();

            return RedirectToAction("Categorias");
        }

        public IActionResult RemoveCategoria(int Id)
        {
            var id = _db.Categorias.Single(c => c.IdCategoria == Id);

            _db.Categorias.Remove(id);
            _db.SaveChanges();

            return RedirectToAction("Categorias");
        }

        //medida
        public IActionResult TiposTamanhos()
        {
            var TiposTamanhos = _db.Medidas.ToList();
            ViewBag.categorias = _db.Categorias.ToList();

            return View(TiposTamanhos);
        }

        [HttpPost]
        public IActionResult TiposTamanhos(Medida med)
        {
            var medida = new Medida
            {
                Nome = med.Nome,
                Sigla = med.Sigla,
                CategoriaId = med.CategoriaId
            };

            if (ModelState.IsValid)
            {
                _db.Medidas.Add(medida);
                _db.SaveChanges();
                return RedirectToAction("TiposTamanhos");

            }

            return View();
        }

        public IActionResult RemoveTiposTamanhos(int Id)
        {
            var id = _db.Medidas.Single(c => c.IdMedida == Id);

            _db.Medidas.Remove(id);
            _db.SaveChanges();

            return RedirectToAction("TiposTamanhos");
        }
    }
}