using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class AdministrativaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdministrativaController(ApplicationDbContext db)
        {
            _db = db;
        }

        //private readonly IHostingEnvironment _appEnvironment;
        //public AdministrativaController(IHostingEnvironment appEnvironment)
        //{
        //    //----< Init: Controller >----
        //    _appEnvironment = appEnvironment;
        //    //----</ Init: Controller >----
        //}



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

        public IActionResult AddProduto()
        {
            IEnumerable<Categoria> categorias = _db.Categorias.ToList();
            ViewModels.Produto viewModel = new ViewModels.Produto
            {
                Categorias = categorias
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddProduto(string nome, double valor, string descricao, string foto, int categoria, int tamanho)
        {
            var produto = new Produto { Nome = nome, Valor = valor, Descricao = descricao, Foto = foto, CategoriaId = categoria, TamanhoId = tamanho };
            if (ModelState.IsValid)
            {
                _db.Produtos.Add(produto);
                _db.SaveChanges();
                return RedirectToAction("AddProduto");
            }
            else
            {
                return View(produto);
            }
        }

        public IActionResult AddInsumo()
        {
            var insumo = _db.Insumo.ToList();
            return View(insumo);
        }

        [HttpPost]
        public IActionResult AddInsumo(string Nome, double Quantidade, string Foto)
        {
            var insumo = new Insumo { Nome = Nome, Quantidade = Quantidade, Foto = Foto };
            return View();
        }

       


    }
}