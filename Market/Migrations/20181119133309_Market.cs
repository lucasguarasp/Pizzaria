using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Market.Migrations
{
    public partial class Market : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    IdEndereco = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cep = table.Column<string>(nullable: false),
                    Rua = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: false),
                    Bairro = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoInsumos",
                columns: table => new
                {
                    IdHistoricoInsumo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    PrecoInsumo = table.Column<double>(nullable: false),
                    DataAdicao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoInsumos", x => x.IdHistoricoInsumo);
                });

            migrationBuilder.CreateTable(
                name: "Insumos",
                columns: table => new
                {
                    IdInsumo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Quantidade = table.Column<double>(nullable: false),
                    PrecoInsumo = table.Column<double>(nullable: false),
                    EstoqueMax = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumos", x => x.IdInsumo);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeUsuarios",
                columns: table => new
                {
                    IdTipoDeUsuario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeUsuarios", x => x.IdTipoDeUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Tamanhos",
                columns: table => new
                {
                    IdTamanho = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanhos", x => x.IdTamanho);
                    table.ForeignKey(
                        name: "FK_Tamanhos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    IdCadastro = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Cpf = table.Column<string>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Celular = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    TipoDeUsuarioId = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastros", x => x.IdCadastro);
                    table.ForeignKey(
                        name: "FK_Cadastros_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cadastros_TipoDeUsuarios_TipoDeUsuarioId",
                        column: x => x.TipoDeUsuarioId,
                        principalTable: "TipoDeUsuarios",
                        principalColumn: "IdTipoDeUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: false),
                    TamanhoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produtos_Tamanhos_TamanhoId",
                        column: x => x.TamanhoId,
                        principalTable: "Tamanhos",
                        principalColumn: "IdTamanho",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoHasInsumos",
                columns: table => new
                {
                    IdProdutoHasInsumo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantidade = table.Column<double>(nullable: false),
                    InsumoId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoHasInsumos", x => x.IdProdutoHasInsumo);
                    table.ForeignKey(
                        name: "FK_ProdutoHasInsumos_Insumos_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumos",
                        principalColumn: "IdInsumo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoHasInsumos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cadastros_EnderecoId",
                table: "Cadastros",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cadastros_TipoDeUsuarioId",
                table: "Cadastros",
                column: "TipoDeUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoHasInsumos_InsumoId",
                table: "ProdutoHasInsumos",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoHasInsumos_ProdutoId",
                table: "ProdutoHasInsumos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_TamanhoId",
                table: "Produtos",
                column: "TamanhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tamanhos_CategoriaId",
                table: "Tamanhos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cadastros");

            migrationBuilder.DropTable(
                name: "HistoricoInsumos");

            migrationBuilder.DropTable(
                name: "ProdutoHasInsumos");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "TipoDeUsuarios");

            migrationBuilder.DropTable(
                name: "Insumos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Tamanhos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
