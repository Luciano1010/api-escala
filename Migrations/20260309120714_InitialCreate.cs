using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscalaApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EhAnciao = table.Column<bool>(type: "bit", nullable: false),
                    EhServo = table.Column<bool>(type: "bit", nullable: false),
                    AprovadoMecanica = table.Column<bool>(type: "bit", nullable: false),
                    AprovadoEnsino = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuncoesFixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuncaoId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncoesFixas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncoesFixas_Funcao_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuncoesFixas_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncoesFixas_FuncaoId",
                table: "FuncoesFixas",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_FuncoesFixas_ParticipanteId",
                table: "FuncoesFixas",
                column: "ParticipanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncoesFixas");

            migrationBuilder.DropTable(
                name: "Funcao");

            migrationBuilder.DropTable(
                name: "Participantes");
        }
    }
}
