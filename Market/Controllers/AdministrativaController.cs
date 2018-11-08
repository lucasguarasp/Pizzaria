using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Models;
using Market.ViewModels;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult CadastroCliente(string Nome, string Email, string Cpf, DateTime DataNascimento, string Telefone, string Celular, string Sexo, string Cep, string Rua, string Numero, string Cidade, string Bairro, string Complemento)

        public IActionResult CadastroCliente(ViewModelCadastro Cadastro)
        {
            var endereco = new Endereco { Cep = Cadastro.Endereco.Cep, Bairro = Cadastro.Endereco.Bairro, Rua = Cadastro.Endereco.Rua, Numero = Cadastro.Endereco.Numero, Cidade = Cadastro.Endereco.Cidade, Complemento = Cadastro.Endereco.Complemento };

            if (ModelState.IsValid)
            {
                _db.Enderecos.Add(endereco);
            }

            var cadastro = new Cadastro { Nome = Cadastro.Cadastro.Nome, Email = Cadastro.Cadastro.Email, Cpf = Cadastro.Cadastro.Cpf, DataNascimento = Cadastro.Cadastro.DataNascimento, Password = Cadastro.Cadastro.Cpf, Celular = Cadastro.Cadastro.Celular, Sexo = Cadastro.Cadastro.Sexo, TipoDeUsuarioId = 1, EnderecoId = endereco.IdEndereco };

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
            IEnumerable<Insumo> insumos = _db.Insumos.ToList();
            ViewModelProduto viewModel = new ViewModelProduto
            {
                Categorias = categorias,
                Insumos = insumos
            };

            return View(viewModel);
        }

        [HttpPost]
        //public IActionResult AddProduto(string nome, double valor, string descricao, string foto, int categoria, int tamanho)
        public IActionResult AddProduto(ViewModelProduto Produto)
        {
            var produto = new Produto { Nome = Produto.Produto.Nome, Valor = Produto.Produto.Valor, Descricao = Produto.Produto.Descricao, Foto = Produto.Produto.Foto, CategoriaId = Produto.Produto.CategoriaId, TamanhoId = Produto.Produto.TamanhoId };
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
            var insumo = _db.Insumos.ToList();
            return View(insumo);
        }

        [HttpPost]
        public IActionResult AddInsumo(string Nome, double Quantidade, double Valor)
        {
            var insumo = new Insumo { Nome = Nome, Quantidade = Quantidade, PrecoInsumo = Valor, EstoqueMax = Quantidade };
            var Historicoinsumo = new HistoricoInsumo { Nome = Nome, Quantidade = Quantidade, PrecoInsumo = Valor, DataAdicao = DateTime.Now };

            _db.Insumos.Add(insumo);
            _db.HistoricoInsumos.Add(Historicoinsumo);

            _db.SaveChanges();
            var insumos = _db.Insumos.ToList();
            return View(insumos);
        }

        
        public IActionResult RemoveInsumo(int Id)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == Id);
            _db.Insumos.Remove(insumo);
            _db.SaveChanges();
            return RedirectToAction("AddInsumo");
        }

        public IActionResult EditInsumo(int Id)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == Id);
            _db.Insumos.Remove(insumo);
            _db.SaveChanges();
            return RedirectToAction("AddInsumo");
        }




    }
}