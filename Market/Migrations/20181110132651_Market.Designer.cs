﻿// <auto-generated />
using Market.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Market.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181110132651_Market")]
    partial class Market
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Market.Models.Cadastro", b =>
                {
                    b.Property<int>("IdCadastro")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celular");

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<DateTime>("DataNascimento");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("EnderecoId");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<string>("Sexo");

                    b.Property<bool>("Status");

                    b.Property<string>("Telefone");

                    b.Property<int>("TipoDeUsuarioId");

                    b.HasKey("IdCadastro");

                    b.HasIndex("EnderecoId");

                    b.HasIndex("TipoDeUsuarioId");

                    b.ToTable("Cadastros");
                });

            modelBuilder.Entity("Market.Models.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("IdCategoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Market.Models.Endereco", b =>
                {
                    b.Property<int>("IdEndereco")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired();

                    b.Property<string>("Cep")
                        .IsRequired();

                    b.Property<string>("Cidade")
                        .IsRequired();

                    b.Property<string>("Complemento");

                    b.Property<string>("Numero");

                    b.Property<string>("Rua")
                        .IsRequired();

                    b.HasKey("IdEndereco");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Market.Models.HistoricoInsumo", b =>
                {
                    b.Property<int>("IdHistoricoInsumo")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataAdicao");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<double>("PrecoInsumo");

                    b.Property<double>("Quantidade");

                    b.HasKey("IdHistoricoInsumo");

                    b.ToTable("HistoricoInsumos");
                });

            modelBuilder.Entity("Market.Models.Insumo", b =>
                {
                    b.Property<int>("IdInsumo")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("EstoqueMax");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<double>("PrecoInsumo");

                    b.Property<double>("Quantidade");

                    b.HasKey("IdInsumo");

                    b.ToTable("Insumos");
                });

            modelBuilder.Entity("Market.Models.Produto", b =>
                {
                    b.Property<int>("IdProduto")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriaId");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<string>("Foto");

                    b.Property<string>("Nome")
                        .IsRequired();

                    b.Property<int?>("TamanhoId");

                    b.Property<double>("Valor");

                    b.HasKey("IdProduto");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("TamanhoId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Market.Models.Tamanho", b =>
                {
                    b.Property<int>("IdTamanho")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoriaId");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("IdTamanho");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Tamanhos");
                });

            modelBuilder.Entity("Market.Models.TipoDeUsuario", b =>
                {
                    b.Property<int>("IdTipoDeUsuario")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.HasKey("IdTipoDeUsuario");

                    b.ToTable("TipoDeUsuarios");
                });

            modelBuilder.Entity("Market.Models.Cadastro", b =>
                {
                    b.HasOne("Market.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Market.Models.TipoDeUsuario", "TipoDeUsuario")
                        .WithMany()
                        .HasForeignKey("TipoDeUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Market.Models.Produto", b =>
                {
                    b.HasOne("Market.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Market.Models.Tamanho", "Tamanho")
                        .WithMany()
                        .HasForeignKey("TamanhoId");
                });

            modelBuilder.Entity("Market.Models.Tamanho", b =>
                {
                    b.HasOne("Market.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");
                });
#pragma warning restore 612, 618
        }
    }
}
