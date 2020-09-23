using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empregados",
                columns: table => new
                {
                    EmpregadoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empregados", x => x.EmpregadoId);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                columns: table => new
                {
                    FaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroFatura = table.Column<string>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    PrecoTotal = table.Column<decimal>(nullable: false),
                    EmpregadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_Faturas_Empregados_EmpregadoId",
                        column: x => x.EmpregadoId,
                        principalTable: "Empregados",
                        principalColumn: "EmpregadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Preco = table.Column<decimal>(nullable: false),
                    UrlImagem = table.Column<string>(nullable: true),
                    EmpregadoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_Produtos_Empregados_EmpregadoId",
                        column: x => x.EmpregadoId,
                        principalTable: "Empregados",
                        principalColumn: "EmpregadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinhasDeFaturas",
                columns: table => new
                {
                    LinhasDeFaturaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    PrecoTotal = table.Column<decimal>(nullable: false),
                    FaturaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinhasDeFaturas", x => x.LinhasDeFaturaId);
                    table.ForeignKey(
                        name: "FK_LinhasDeFaturas_Faturas_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Faturas",
                        principalColumn: "FaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinhasDeFaturas_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "EmpregadoId", "Email", "Nome" },
                values: new object[] { 1, "kevin.lourenco@hotmail.com", "Kevin Lourenco" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ProdutoId", "Descricao", "EmpregadoId", "Nome", "ParentId", "Preco", "UrlImagem" },
                values: new object[] { 1, "", 1, "Produto1", null, 20m, null });

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_EmpregadoId",
                table: "Faturas",
                column: "EmpregadoId");

            migrationBuilder.CreateIndex(
                name: "IX_LinhasDeFaturas_FaturaId",
                table: "LinhasDeFaturas",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_LinhasDeFaturas_ProdutoId",
                table: "LinhasDeFaturas",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpregadoId",
                table: "Produtos",
                column: "EmpregadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinhasDeFaturas");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Empregados");
        }
    }
}
