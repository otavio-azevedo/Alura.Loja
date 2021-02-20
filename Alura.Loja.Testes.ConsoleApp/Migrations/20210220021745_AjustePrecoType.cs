using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alura.Loja.Testes.ConsoleApp.Migrations
{
    public partial class AjustePrecoType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoUnitario",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Compras",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PrecoUnitario",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Compras",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
