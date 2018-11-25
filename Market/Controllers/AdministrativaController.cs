using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    //[Authorize]
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

            var cadastro = new Usuario
            {
                Nome = Cad.Cadastro.Nome,
                Email = Cad.Cadastro.Email,
                Cpf = Cad.Cadastro.Cpf,
                DataNascimento = Cad.Cadastro.DataNascimento,
                Password = HashService.Crip(Cad.Cadastro.Cpf),
                Telefone = Cad.Cadastro.Telefone,
                Celular = Cad.Cadastro.Celular,
                Sexo = Cad.Cadastro.Sexo,
                TipoDeUsuarioId = 2,
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

        public IActionResult AddProduto()
        {
            ViewBag.categorias = _db.Categorias.ToList();
            ViewBag.insumos = _db.Insumos.ToList();

            return View();

        }

        [HttpPost]
        public IActionResult AddProduto(string Nome, double Valor, int CategoriaId, string[] descricao, IFormFile Foto, double Quantidade)
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
                Nome = Nome,
                Valor = Valor,
                Descricao = lista,
                CategoriaId = CategoriaId
            };

            Task<string> foto = ImagemService.SalvarImagemProduto(Foto);


            if (foto.Result != null)
            {
                produto.Foto = foto.Result;
            }

            _db.Produtos.Add(produto);

            foreach (var item in descricao)
            {
                var itemId = _db.Insumos.Single(i => i.Nome.Equals(item));

                var produtoInsumo = new ProdutoHasInsumo
                {
                    ProdutoId = produto.IdProduto,
                    InsumoId = itemId.IdInsumo,
                    Quantidade = Quantidade
                };

                _db.ProdutoHasInsumos.Add(produtoInsumo);
            }

            if (ModelState.IsValid)
            {
                _db.SaveChanges();
            }

            return RedirectToAction("AddProduto");
        }

        public IActionResult ListarProdutos()
        {
            var produto = _db.Produtos.Include(c => c.Categoria).ToList();
           // ViewData["ProdutoHasInsumo"] = _db.ProdutoHasInsumos.Include(i => i.Insumo).ToList();

            if (produto != null)
            {
                return View(produto);
            }
            else
            {
                return View();
            }

        }

        public JsonResult ProdutoHasInsumo(int Id)
        {
            var ProdutoHasInsumo  = _db.ProdutoHasInsumos.Include(i => i.Insumo).Where(c => c.ProdutoId==Id).ToList();

            return Json(ProdutoHasInsumo);
        }

        public IActionResult RemoveProduto(int Id)
        {
            var produto = _db.Produtos.Single(x => x.IdProduto == Id);
            _db.Produtos.Remove(produto);

            if (produto.Foto != null)
            {
                var File = string.Concat(@"wwwroot", produto.Foto.Replace("~", ""));

                if (System.IO.File.Exists(File))
                {
                    System.IO.File.Delete(File);
                }
            }

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



    }
}