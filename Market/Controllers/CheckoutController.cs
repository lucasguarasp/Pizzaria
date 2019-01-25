using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Checkout(int id)
        {
        //    var cookieFromHeaderString = (HttpContext.Request.Headers["Cookie"]).FirstOrDefault();
        //    if (cookieFromHeaderString != null)
        //    {

        //        string[] strArray = cookieFromHeaderString.Split(new string[] { "; " }, StringSplitOptions.None);
        //        string whCookie = strArray.Where(m => m.StartsWith("Projeto=")).FirstOrDefault();

        //        if (whCookie != null)
        //        {
        //            int start = whCookie.IndexOf("=") + 1;
        //            string cookieValue = whCookie.Substring(start);

        //            string[] whArray = cookieValue.Split('}');                    
                    
        //        }
        //    }

           return View();
        }

       
       [HttpPost]
         public IActionResult Checkout(List<ViewModelCheckout> produtos)
        {
            //return RedirectToAction("Checkout");            
           return Json (produtos);
        }
    }
}

