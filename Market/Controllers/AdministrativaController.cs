using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Extensions.Options;

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

            ViewBag.categorias = _db.Categorias.ToList();
            ViewBag.insumos = _db.Insumos.ToList();

            return View();

        }

        [HttpPost]
        public IActionResult AddProduto(ViewModelProduto Prod, string[] descricao)
        {
            var lista = "";
            foreach (var item in descricao)
            {
                if (item == descricao.Last())
                {
                    lista += item;
                }
                else
                {
                    lista += item + ", ";
                }
            }

            var produto = new Produto
            {
                Nome = Prod.Produto.Nome,
                Valor = Prod.Produto.Valor,
                Descricao = lista,
                Foto = Prod.Produto.Foto,
                CategoriaId = Prod.Produto.CategoriaId
            };

            _db.Produtos.Add(produto);

            //if (ModelState.IsValid)
            //{
                _db.SaveChanges();
            //}

            return RedirectToAction("AddProduto");
        }

        public IActionResult RemoveProduto(int Id)
        {

            var produto = _db.Produtos.Single(x => x.IdProduto == Id);
            _db.Produtos.Remove(produto);
            _db.SaveChanges();
            return RedirectToAction("ListarProdutos");

        }


        public IActionResult AddInsumo(int Id)
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

            return RedirectToAction("AddInsumo");
        }


        public IActionResult RemoveInsumo(int Id)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == Id);
            _db.Insumos.Remove(insumo);
            _db.SaveChanges();
            return RedirectToAction("AddInsumo");
        }

        [HttpPost]
        public IActionResult EditInsumo(int EditId, string EditNome, double EditQuantidade, string option)
        {
            var insumo = _db.Insumos.Single(x => x.IdInsumo == EditId);

            switch (option)
            {
                case "adicionar":
                    if (insumo != null)
                    {
                        insumo.Nome = EditNome;
                        insumo.Quantidade += EditQuantidade;
                        insumo.EstoqueMax += EditQuantidade;
                        _db.Entry(insumo).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    break;

                case "remover":
                    if (insumo != null)
                    {
                        insumo.Nome = EditNome;
                        insumo.Quantidade -= EditQuantidade;
                        insumo.EstoqueMax -= EditQuantidade;
                        _db.Entry(insumo).State = EntityState.Modified;
                        _db.SaveChanges();
                    }
                    break;

            }

            return RedirectToAction("AddInsumo");
        }


        public IActionResult ListarProdutos()
        //public JsonResult ListarProdutos(string tipo)
        {
            ViewBag.categorias = _db.Categorias.ToList();
            var produto = _db.Produtos.Include(c => c.Categoria).ToList();

            //int t = Convert.ToInt32(tipo);            

            //IEnumerable<Produto> produtos = _db.Produtos.Where(p => p.IdProduto == t).Include(c => c.Categoria).ToList();

            //if (tipo != null)
            //{
            //    var produto = _db.Produtos.Where(p => p.IdProduto == t).Include(c => c.Categoria).ToList();
            //}
            //else
            //{
            //    var produto = _db.Produtos.Include(c => c.Categoria).ToList();
            //}
            //ViewBag.Produtos = produtos.ToList();
            //return Json(ViewBag.Produtos);

            if (produto != null)
            {
                return View(produto);
            }
            else
            {
                return View();
            }

        }




    }
}