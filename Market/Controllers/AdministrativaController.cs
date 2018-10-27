using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;


namespace Market.Controllers
{
    public class AdministrativaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {          
            return View();
        }        
        

    }
}