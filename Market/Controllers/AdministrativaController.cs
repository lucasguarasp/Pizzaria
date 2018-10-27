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
        //private ApplicationDbContext db = new ApplicationDbContext();

        //private readonly ApplicationDbContext db = new ApplicationDbContext();

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
        public IActionResult CadastroCliente(string Nome, string Email, int Cpf, DateTime DataNascimento, string Login, string Password, int Telefone, int Celular, string Sexo, int Cep, string Rua, int Numero, string Cidade, string Bairro, string Complemento)
        {
            var cadastro = new Cadastro { Nome = Nome, Email = Email, Cpf = Cpf, DataNascimento = DataNascimento, Login = Login, Password = Password, Celular = Celular, Sexo = Sexo };
            var endereco = new Endereco { Cep= Cep, Bairro = Bairro, Rua = Rua, Numero = Numero, Cidade =Cidade, Complemento = Complemento};
            var tipoUsuario = new TipoDeUsuario { Descricao = "Usuário" };
            _db.Cadastros.Add(cadastro);
            //_db.Enderecos.Add(endereco);
            //_db.TipoDeUsuarios.Add(tipoUsuario);
            _db.SaveChanges();

            return View();
        }        
        

    }
}