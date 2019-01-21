using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
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
    }
}