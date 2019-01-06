using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Market.Models;
using Market.Services;
using Market.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Market.Controllers
{
    [Authorize]
    public class AdministrativaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdministrativaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {            

            //pega lista
            //HttpContext.User.Claims
            //var nome = HttpContext.User.Claims.FirstOrDefault(u => u.Type==ClaimTypes.Name);
            var nome = HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value;
            ViewBag.Nome = nome;
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        [Authorize(Roles = "gerente")]
        public IActionResult Teste()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente(ViewModelCadastro Cad)
        {
            var endereco = new Endereco { Cep = Cad.Endereco.Cep, Bairro = Cad.Endereco.Bairro, Rua = Cad.Endereco.Rua, Numero = Cad.Endereco.Numero, Cidade = Cad.Endereco.Cidade, Complemento = Cad.Endereco.Complemento };
            _db.Enderecos.Add(endereco);

            var cadastro = new Usuario
            {
                Nome = Cad.Cadastro.Nome,
                Email = Cad.Cadastro.Email,
                Cpf = Cad.Cadastro.Cpf.Replace(".", "").Replace("-", ""),
                DataNascimento = Cad.Cadastro.DataNascimento,
                Password = HashService.Crip(Cad.Cadastro.Cpf.Replace(".", "").Replace("-", "")),
                Telefone = Cad.Cadastro.Telefone,
                Celular = Cad.Cadastro.Celular,
                Sexo = Cad.Cadastro.Sexo,
                TipoDeUsuarioId = 3,
                EnderecoId = endereco.IdEndereco,
                Status = true
            };

            _db.Cadastros.Add(cadastro);

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }

            return RedirectToAction("CadastroCliente");
        }

    }
}