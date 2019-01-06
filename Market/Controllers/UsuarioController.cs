using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Market.Models;
using Market.Services;
using Market.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                return RedirectToAction("index", "Administrativa");
            }

            if (_db.Cadastros.Count(u => u.Email.Equals(usuarioLogin.Login) || u.Cpf.Equals(usuarioLogin.Login)) > 0)
            {
                var usuario = _db.Cadastros.Include(c => c.TipoDeUsuario).Single(u => u.Email.Equals(usuarioLogin.Login) || u.Cpf.Equals(usuarioLogin.Login));

                if (usuario.Password.Equals(HashService.Crip(usuarioLogin.Senha)) && usuario.Status != false)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim("Cpf", usuario.Cpf),
                        new Claim(ClaimTypes.Role, usuario.TipoDeUsuario.Descricao)
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties =
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("index", "Administrativa");
                }
            }

            return View(usuarioLogin);

        }


        public IActionResult Logout()
        {
            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                HttpContext.SignOutAsync();
            }

            return RedirectToAction("Login");
        }
    }
}