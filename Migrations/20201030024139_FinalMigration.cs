using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMarket.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Saidas");

            migrationBuilder.AddColumn<float>(
                name: "ValorPago",
                table: "Vendas",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saidas",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VendaId",
                table: "Saidas",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Estoques",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Estoques",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Categorias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Saidas_Vendas_VendaId",
                table: "Saidas");

            migrationBuilder.DropIndex(
                name: "IX_Saidas_VendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "ValorPago",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "VendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Categorias");

            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Vendas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Saidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Estoques",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
