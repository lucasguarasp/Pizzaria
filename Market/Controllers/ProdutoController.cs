using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Market.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProdutoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduto(int cat)
        {
            if (cat != 0)
            {
                //ViewBag.insumos = new MultiSelectList(_db.Insumos.ToList(), "Nome", "Nome");
                var insumos = _db.InsumoHasCategorias.Where(i => i.CategoriaId == cat).Include(x => x.Insumo).ToList();
                var Medidas = _db.Medidas.Where(m => m.CategoriaId == cat).ToList();

                var result = new { data1 = Medidas, data2 = insumos };
                return Json(result);
            }
            ViewBag.categorias = new SelectList(_db.Categorias.ToList(), "IdCategoria", "Descricao");
           
            return View();

        }

        [HttpPost]
        public IActionResult AddProduto(string Nome, double Valor, int CategoriaId, int TamanhoId, string[] descricao, IFormFile Foto, string[] Quantidade)
        {

            //coloca nome todos insumos add em uma lista para preencher descricao em produto
            var lista = "";
            foreach (var item in descricao)
            {
                if (item == descricao.Last())
                {
                    lista += item;
                }
                else
                {
                    lista += item + ", ";
                }
            }

            //adiciona produto
            var produto = new Produto
            {
                Nome = Nome,
                Valor = Valor,
                Descricao = lista,
                CategoriaId = CategoriaId,
                TamanhoId = TamanhoId

            };

            Task<string> foto = ImagemService.SalvarImagemProduto(Foto);

            if (foto.Result != null)
            {
                produto.Foto = foto.Result;
            }else{
                produto.Foto = @"~/images/ImgEmpty/noImg.jpg";
            }

            //terminio do add produto
            _db.Produtos.Add(produto);

            //adiciona insumos na tabela Produto tem insumos 
            int cont = 0;
            foreach (var item in descricao)
            {
                var itemId = _db.Insumos.Single(i => i.Nome.Equals(item));

                var produtoInsumo = new ProdutoHasInsumo
                {
                    ProdutoId = produto.IdProduto,
                    InsumoId = itemId.IdInsumo,
                    Quantidade = Convert.ToDouble(Quantidade[cont])
                };

                _db.ProdutoHasInsumos.Add(produtoInsumo);
                cont++;
            }

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }

            return RedirectToAction("AddProduto");
        }

        public IActionResult ListarProdutos()
        {
            var produto = _db.Produtos.Include(c => c.Categoria).Include(m => m.Tamanho).ToList();

            if (produto != null)
            {
                return View(produto);
            }
            else
            {
                return View();
            }
        }

        public JsonResult ProdutoHasInsumo(int Id)
        {
            var ProdutoHasInsumo = _db.ProdutoHasInsumos.Include(i => i.Insumo).Where(c => c.ProdutoId == Id).ToList();

            return Json(ProdutoHasInsumo);
        }

        public IActionResult RemoveProduto(int Id)
        {
            var produto = _db.Produtos.Single(x => x.IdProduto == Id);
            _db.Produtos.Remove(produto);

            if (produto.Foto != "~/images/ImgEmpty/noImg.jpg")
            {
                var File = string.Concat(@"wwwroot", produto.Foto.Replace("~", ""));

                if (System.IO.File.Exists(File))
                {
                    System.IO.File.Delete(File);
                }
            }

            _db.SaveChanges();
            return RedirectToAction("ListarProdutos");
        }

        public IActionResult AddInsumo()
        {
            ViewBag.categorias = new SelectList(_db.Categorias.ToList(), "IdCategoria", "Descricao");

            var insumo = _db.Insumos.ToList();
            return View(insumo);
        }

        [HttpPost]
        public IActionResult AddInsumo(string Nome, double Quantidade, double Valor, int[] Categorias)
        {

            var insumo = new Insumo { Nome = Nome, Quantidade = Quantidade, PrecoInsumo = Valor, EstoqueMax = Quantidade };
            var Historicoinsumo = new HistoricoInsumo { Nome = Nome, Quantidade = Quantidade, PrecoInsumo = Valor, DataAdicao = DateTime.Now };

            _db.Insumos.Add(insumo);
            _db.HistoricoInsumos.Add(Historicoinsumo);

            foreach (var item in Categorias)
            {
                var InsumoHasCategoria = new InsumoHasCategoria { InsumoId = insumo.IdInsumo, CategoriaId = item };
                _db.InsumoHasCategorias.Add(InsumoHasCategoria);
            }

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }

            return RedirectToAction("AddInsumo");
        }


        public IActionResult RemoveInsumo(int Id)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == Id);
            _db.Insumos.Remove(insumo);
            _db.SaveChanges();
            return RedirectToAction("AddInsumo");
        }


        public IActionResult ShowInsumos(int EditId)
        {
            var InsumoHasCategoria = _db.InsumoHasCategorias.Where(i => i.InsumoId == EditId).Include(x => x.Categoria).ToList();

            return Json(InsumoHasCategoria);
        }

        [HttpPost]
        public IActionResult EditInsumo(int EditId, string EditNome, double EditQuantidade, string option)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == EditId);

            switch (option)
            {
                case "adicionar":
                    if (insumo != null)
                    {
                        insumo.Nome = EditNome;
                        insumo.Quantidade += EditQuantidade;
                        insumo.EstoqueMax += EditQuantidade;
                        _db.Entry(insumo).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    break;

                case "remover":
                    if (insumo != null)
                    {
                        insumo.Nome = EditNome;
                        insumo.Quantidade -= EditQuantidade;
                        insumo.EstoqueMax -= EditQuantidade;
                        _db.Entry(insumo).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    break;
            }

            return RedirectToAction("AddInsumo");
        }



    }
}