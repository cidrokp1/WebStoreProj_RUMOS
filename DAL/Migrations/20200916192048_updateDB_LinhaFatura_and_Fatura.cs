using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class updateDB_LinhaFatura_and_Fatura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturas_Empregados_EmpregadoId",
                table: "Faturas");

            migrationBuilder.DropForeignKey(
                name: "FK_LinhasDeFaturas_Faturas_FaturaId",
                table: "LinhasDeFaturas");

            migrationBuilder.AlterColumn<int>(
                name: "FaturaId",
                table: "LinhasDeFaturas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmpregadoId",
                table: "Faturas",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturas_Empregados_EmpregadoId",
                table: "Faturas",
                column: "EmpregadoId",
                principalTable: "Empregados",
                principalColumn: "EmpregadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinhasDeFaturas_Faturas_FaturaId",
                table: "LinhasDeFaturas",
                column: "FaturaId",
                principalTable: "Faturas",
                principalColumn: "FaturaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faturas_Empregados_EmpregadoId",
                table: "Faturas");

            migrationBuilder.DropForeignKey(
                name: "FK_LinhasDeFaturas_Faturas_FaturaId",
                table: "LinhasDeFaturas");

            migrationBuilder.AlterColumn<int>(
                name: "FaturaId",
                table: "LinhasDeFaturas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpregadoId",
                table: "Faturas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Faturas_Empregados_EmpregadoId",
                table: "Faturas",
                column: "EmpregadoId",
                principalTable: "Empregados",
                principalColumn: "EmpregadoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinhasDeFaturas_Faturas_FaturaId",
                table: "LinhasDeFaturas",
                column: "FaturaId",
                principalTable: "Faturas",
                principalColumn: "FaturaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
