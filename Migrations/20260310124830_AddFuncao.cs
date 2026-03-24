using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscalaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFuncao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncoesFixas_Funcao_FuncaoId",
                table: "FuncoesFixas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcao",
                table: "Funcao");

            migrationBuilder.RenameTable(
                name: "Funcao",
                newName: "Funcoes");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Funcoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Funcoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcoes",
                table: "Funcoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncoesFixas_Funcoes_FuncaoId",
                table: "FuncoesFixas",
                column: "FuncaoId",
                principalTable: "Funcoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FuncoesFixas_Funcoes_FuncaoId",
                table: "FuncoesFixas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcoes",
                table: "Funcoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Funcoes");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Funcoes");

            migrationBuilder.RenameTable(
                name: "Funcoes",
                newName: "Funcao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcao",
                table: "Funcao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FuncoesFixas_Funcao_FuncaoId",
                table: "FuncoesFixas",
                column: "FuncaoId",
                principalTable: "Funcao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
