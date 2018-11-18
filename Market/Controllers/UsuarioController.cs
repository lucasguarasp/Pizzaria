using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Market.Services;
using Market.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UsuarioController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewModelUsuarioLogin usuarioLogin)
        {
            var usuario = usuarioLogin.Entrar(_db);
            if (!ModelState.IsValid) return View(usuarioLogin);


            if (usuario.Password.Equals(HashService.Crip(usuarioLogin.Senha)) && usuario.Status != false)
            {               
                CookieOptions cookies = new CookieOptions();
                cookies.Expires = DateTime.Now.AddMinutes(30);
                return RedirectToAction("index", "Administrativa");
            }

            return View(usuarioLogin);

        }
    }
}