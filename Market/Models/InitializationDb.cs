using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.Models
{
    public class InitializationDb
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categorias.Any())
            {
                return;
            }

            var categoria = new Categoria[]
            {
                new Categoria{Descricao="Pizza"},
                new Categoria{Descricao="Lanche"},
                new Categoria{Descricao="Bebida"},
                new Categoria{Descricao="Refeição"}
            };

            //add uma linsta Range            
            context.Categorias.AddRange(categoria);


            var medida = new Medida[]
            {
                new Medida{Nome="Pequena", Sigla="P", CategoriaId = 1},
                new Medida{Nome="Média", Sigla="M", CategoriaId = 1},
                new Medida{Nome="Grande", Sigla="G", CategoriaId = 1},
                new Medida{Nome="Gigante", Sigla="GG", CategoriaId = 1},
                new Medida{Nome="Unidade", Sigla="UN", CategoriaId = 2},
                new Medida{Nome="Unidade", Sigla="UN", CategoriaId = 3}
            };

            foreach (var item in medida)
            {
                context.Medidas.Add(item);
            }

            var insumo = new Insumo[]
            {
                new Insumo{Nome="Tomate", PrecoInsumo = 1.99 , EstoqueMax = 200, Quantidade = 200},
                new Insumo{Nome="Cebola", PrecoInsumo = 2.99 , EstoqueMax = 300, Quantidade = 300},
                new Insumo{Nome="Batata Palha", PrecoInsumo = 3.99 , EstoqueMax = 100, Quantidade = 100},
                new Insumo{Nome="Queijo", PrecoInsumo = 0.99 , EstoqueMax = 150, Quantidade = 150},
                new Insumo{Nome="Presunto", PrecoInsumo = 1.50 , EstoqueMax = 200, Quantidade = 200}
            };

            foreach (var item in insumo)
            {
                context.Insumos.Add(item);
            }

            var tipousuario = new TipoDeUsuario[]
            {
                new TipoDeUsuario{Descricao="gerente"},
                new TipoDeUsuario{Descricao="vendedor"},
                new TipoDeUsuario{Descricao="cliente"}
            };

            foreach (var item in tipousuario)
            {
                context.TipoDeUsuarios.Add(item);
            }

            context.SaveChanges();

            var insumoCategoria = new InsumoHasCategoria[]
            {
                new InsumoHasCategoria{InsumoId = 1, CategoriaId = 1},
                new InsumoHasCategoria{InsumoId = 1, CategoriaId = 2},
                new InsumoHasCategoria{InsumoId = 2, CategoriaId = 1},
                new InsumoHasCategoria{InsumoId = 3, CategoriaId = 1},
                new InsumoHasCategoria{InsumoId = 3, CategoriaId = 2},
                new InsumoHasCategoria{InsumoId = 4, CategoriaId = 1},
                new InsumoHasCategoria{InsumoId = 4, CategoriaId = 2},
                new InsumoHasCategoria{InsumoId = 5, CategoriaId = 2},
                new InsumoHasCategoria{InsumoId = 1, CategoriaId = 4}
            };

            foreach (var item in insumoCategoria)
            {
                context.InsumoHasCategorias.Add(item);
            }

            context.SaveChanges();

        }

    }
}
