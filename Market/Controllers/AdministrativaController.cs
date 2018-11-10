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
        public IActionResult CadastroCliente(ViewModelCadastro Cad)
        {
            var endereco = new Endereco { Cep = Cad.Endereco.Cep, Bairro = Cad.Endereco.Bairro, Rua = Cad.Endereco.Rua, Numero = Cad.Endereco.Numero, Cidade = Cad.Endereco.Cidade, Complemento = Cad.Endereco.Complemento };
            _db.Enderecos.Add(endereco);

            var cadastro = new Cadastro { Nome = Cad.Cadastro.Nome, Email = Cad.Cadastro.Email, Cpf = Cad.Cadastro.Cpf, DataNascimento = Cad.Cadastro.DataNascimento, Password = Cad.Cadastro.Cpf, Telefone = Cad.Cadastro.Telefone, Celular = Cad.Cadastro.Celular, Sexo = Cad.Cadastro.Sexo, TipoDeUsuarioId = 2, EnderecoId = endereco.IdEndereco, Status = true };
            _db.Cadastros.Add(cadastro);

            if (ModelState.IsValid)
            {
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
        public IActionResult AddProduto(ViewModelProduto Prod)
        {
            var produto = new Produto
            {
                Nome = Prod.Produto.Nome,
                Valor = Prod.Produto.Valor,
                Descricao = Prod.Produto.Descricao,
                Foto = Prod.Produto.Foto,
                CategoriaId = Prod.Produto.CategoriaId
            };

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


            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }
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