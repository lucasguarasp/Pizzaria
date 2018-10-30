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

        //protected override void Seed(Market.Models.ApplicationDbContext context)
        //{

        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<TipoDeUsuario>().HasData(
        //        new TipoDeUsuario() { IdTipoDeUsuario = 1 , Descricao = "Gerente"});
        //}


        public virtual DbSet<Cadastro> Cadastros { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<TipoDeUsuario> TipoDeUsuarios { get; set; }

        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Tamanho> Tamanhos { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }


    }
}
