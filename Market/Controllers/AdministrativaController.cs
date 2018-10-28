using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class AdministrativaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdministrativaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente(string Nome, string Email, string Cpf, DateTime DataNascimento, string Telefone, string Celular, string Sexo, string Cep, string Rua, string Numero, string Cidade, string Bairro, string Complemento)
        {
            var endereco = new Endereco { Cep = Cep, Bairro = Bairro, Rua = Rua, Numero = Numero, Cidade = Cidade, Complemento = Complemento };

            if (ModelState.IsValid)
            {
                _db.Enderecos.Add(endereco);
            }

            var cadastro = new Cadastro { Nome = Nome, Email = Email, Cpf = Cpf, DataNascimento = DataNascimento, Password = Cpf, Celular = Celular, Sexo = Sexo, TipoDeUsuarioId = 1, EnderecoId = endereco.IdEndereco };

            if (ModelState.IsValid)
            {
                _db.Cadastros.Add(cadastro);
                _db.SaveChanges();
            }

            return RedirectToAction("CadastroCliente");

        }
    }
}