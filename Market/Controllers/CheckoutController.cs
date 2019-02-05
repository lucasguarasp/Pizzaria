using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Market.Models;
using Market.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CheckoutController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (TempData["Cookie"] != null)
            {
                string tp = TempData["Cookie"] as String;
            }
            var produto = _db.Produtos.ToList();

            if (produto != null)
            {
                ViewBag.produto = produto;
                return View(ViewBag.produto);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(List<ViewModelCheckout> produtos, bool finaliza)
        {
            if (finaliza == true)
            {//true finaliza a campra e volta para pedidos
                var insumoHasProd = new List<ProdutoHasInsumo>();

                for (int i = 0, j; i < produtos.Count; i++)
                {
                    j = 0;
                    insumoHasProd = _db.ProdutoHasInsumos.Include(s => s.Insumo).Where(c => c.ProdutoId == produtos[i].Id).ToList();
                    foreach (var item in insumoHasProd)//Tira quantididade de insumo do produto no estoque
                    {
                        var insumo = _db.Insumos.Single(a => a.IdInsumo == insumoHasProd[j].InsumoId);
                        insumo.Quantidade -= insumoHasProd[j].Quantidade * produtos[i].Quantidade;
                        _db.Entry(insumo).State = EntityState.Modified;
                        _db.SaveChanges();
                        j++;
                    }
                }

                // foreach (var cookie in Request.Cookies.Keys)
                // {
                //     if (cookie == "Projeto")
                //     {
                //         Response.Cookies.Delete(cookie);
                //     }
                // }
                return View();
            }
            else
            {//false apenas retorna a lista de produtos com as imagens
                foreach (var item in produtos)
                {
                    item.Imagem = _db.Produtos.Single(c => c.IdProduto == item.Id).Foto;
                    item.Descricao = _db.Produtos.Single(c => c.IdProduto == item.Id).Descricao;
                }

                return Json(produtos);
            }
        }

        [HttpPost]
        public IActionResult FinalizaPedido()
        {
            var cookie = Request.Cookies;

            return RedirectToAction("Index", "Checkout");
        }

    }
}

