using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Market.Models;


namespace Market.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Cadastros { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<TipoDeUsuario> TipoDeUsuarios { get; set; }

        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Medida> Tamanhos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Insumo> Insumos { get; set; }
        public virtual DbSet<HistoricoInsumo> HistoricoInsumos { get; set; }
        public virtual DbSet<ProdutoHasInsumo> ProdutoHasInsumos { get; set; }
        public virtual DbSet<Imagem> Imagems { get; set; }


    }
}
