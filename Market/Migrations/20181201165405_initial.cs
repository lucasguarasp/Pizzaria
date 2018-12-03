using Microsoft.EntityFrameworkCore.Migrations;

namespace Market.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tamanhos_Categorias_CategoriaId",
                table: "Tamanhos");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Tamanhos",
                newName: "Sigla");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Tamanhos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Tamanhos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tamanhos_Categorias_CategoriaId",
                table: "Tamanhos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tamanhos_Categorias_CategoriaId",
                table: "Tamanhos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Tamanhos");

            migrationBuilder.RenameColumn(
                name: "Sigla",
                table: "Tamanhos",
                newName: "Descricao");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Tamanhos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tamanhos_Categorias_CategoriaId",
                table: "Tamanhos",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
