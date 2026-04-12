using System;
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
                name: "Escalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipoExecucao = table.Column<int>(type: "int", nullable: false),
                    QuantidadePessoas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    EscalaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcoes_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GruposEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GruposEscala_Funcoes_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    EscalaId = table.Column<int>(type: "int", nullable: true),
                    GrupoEscalaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participantes_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participantes_GruposEscala_GrupoEscalaId",
                        column: x => x.GrupoEscalaId,
                        principalTable: "GruposEscala",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EscalaItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaId = table.Column<int>(type: "int", nullable: false),
                    FuncaoId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalaItems_Escalas_EscalaId",
                        column: x => x.EscalaId,
                        principalTable: "Escalas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalaItems_Funcoes_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalaItems_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipanteFuncaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    FuncaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteFuncaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipanteFuncaos_Funcoes_FuncaoId",
                        column: x => x.FuncaoId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipanteFuncaos_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipanteGrupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    GrupoEscalaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipanteGrupos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParticipanteGrupos_GruposEscala_GrupoEscalaId",
                        column: x => x.GrupoEscalaId,
                        principalTable: "GruposEscala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipanteGrupos_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EscalaItems_EscalaId",
                table: "EscalaItems",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaItems_FuncaoId",
                table: "EscalaItems",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalaItems_ParticipanteId",
                table: "EscalaItems",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcoes_EscalaId",
                table: "Funcoes",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_GruposEscala_FuncaoId",
                table: "GruposEscala",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteFuncaos_FuncaoId",
                table: "ParticipanteFuncaos",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteFuncaos_ParticipanteId",
                table: "ParticipanteFuncaos",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteGrupos_GrupoEscalaId",
                table: "ParticipanteGrupos",
                column: "GrupoEscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipanteGrupos_ParticipanteId",
                table: "ParticipanteGrupos",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_EscalaId",
                table: "Participantes",
                column: "EscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_GrupoEscalaId",
                table: "Participantes",
                column: "GrupoEscalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EscalaItems");

            migrationBuilder.DropTable(
                name: "ParticipanteFuncaos");

            migrationBuilder.DropTable(
                name: "ParticipanteGrupos");

            migrationBuilder.DropTable(
                name: "Participantes");

            migrationBuilder.DropTable(
                name: "GruposEscala");

            migrationBuilder.DropTable(
                name: "Funcoes");

            migrationBuilder.DropTable(
                name: "Escalas");
        }
    }
}
