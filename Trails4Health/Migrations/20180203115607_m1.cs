using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Trails4Health.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dificuldades",
                columns: table => new
                {
                    DificuldadeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dificuldades", x => x.DificuldadeID);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "Guias",
                columns: table => new
                {
                    GuiaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Avaliacao = table.Column<float>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guias", x => x.GuiaID);
                });

            migrationBuilder.CreateTable(
                name: "Opcoes",
                columns: table => new
                {
                    OpcaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NumeroOpcao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opcoes", x => x.OpcaoID);
                });

            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    QuestaoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Desactivada = table.Column<bool>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    NomeQuestao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.QuestaoID);
                });

            migrationBuilder.CreateTable(
                name: "Trilhos2",
                columns: table => new
                {
                    TrilhoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Distancia = table.Column<double>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trilhos2", x => x.TrilhoID);
                });

            migrationBuilder.CreateTable(
                name: "Turistas",
                columns: table => new
                {
                    TuristaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Morada = table.Column<string>(nullable: true),
                    Nif = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turistas", x => x.TuristaID);
                });

            migrationBuilder.CreateTable(
                name: "Trilhos",
                columns: table => new
                {
                    TrilhoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Desativado = table.Column<bool>(nullable: false),
                    Detalhes = table.Column<string>(maxLength: 700, nullable: false),
                    DificuldadeID = table.Column<int>(nullable: false),
                    Distancia = table.Column<double>(nullable: false),
                    Fim = table.Column<string>(maxLength: 50, nullable: false),
                    Foto = table.Column<byte[]>(nullable: true),
                    Inicio = table.Column<string>(maxLength: 50, nullable: false),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Sumario = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trilhos", x => x.TrilhoID);
                    table.ForeignKey(
                        name: "FK_Trilhos_Dificuldades_DificuldadeID",
                        column: x => x.DificuldadeID,
                        principalTable: "Dificuldades",
                        principalColumn: "DificuldadeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservasGuia",
                columns: table => new
                {
                    ReservaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuiaID = table.Column<int>(nullable: false),
                    ReservaParaDia = table.Column<DateTime>(nullable: false),
                    TrilhoID = table.Column<int>(nullable: false),
                    TuristaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasGuia", x => x.ReservaID);
                    table.ForeignKey(
                        name: "FK_ReservasGuia_Guias_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "Guias",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservasGuia_Trilhos2_TrilhoID",
                        column: x => x.TrilhoID,
                        principalTable: "Trilhos2",
                        principalColumn: "TrilhoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservasGuia_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasAvaliacao",
                columns: table => new
                {
                    RespostaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    GuiaID = table.Column<int>(nullable: false),
                    OpcaoID = table.Column<int>(nullable: false),
                    QuestaoID = table.Column<int>(nullable: false),
                    TuristaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasAvaliacao", x => x.RespostaID);
                    table.ForeignKey(
                        name: "FK_RespostasAvaliacao_Guias_GuiaID",
                        column: x => x.GuiaID,
                        principalTable: "Guias",
                        principalColumn: "GuiaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasAvaliacao_Opcoes_OpcaoID",
                        column: x => x.OpcaoID,
                        principalTable: "Opcoes",
                        principalColumn: "OpcaoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasAvaliacao_Questoes_QuestaoID",
                        column: x => x.QuestaoID,
                        principalTable: "Questoes",
                        principalColumn: "QuestaoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasAvaliacao_Turistas_TuristaID",
                        column: x => x.TuristaID,
                        principalTable: "Turistas",
                        principalColumn: "TuristaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadoTrilhos",
                columns: table => new
                {
                    EstadoTrilhoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataFim = table.Column<DateTime>(nullable: true),
                    DataInicio = table.Column<DateTime>(nullable: true),
                    EstadoID = table.Column<int>(nullable: false),
                    TrilhoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoTrilhos", x => x.EstadoTrilhoID);
                    table.ForeignKey(
                        name: "FK_EstadoTrilhos_Estados_EstadoID",
                        column: x => x.EstadoID,
                        principalTable: "Estados",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoTrilhos_Trilhos_TrilhoID",
                        column: x => x.TrilhoID,
                        principalTable: "Trilhos",
                        principalColumn: "TrilhoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoTrilhos_EstadoID",
                table: "EstadoTrilhos",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoTrilhos_TrilhoID",
                table: "EstadoTrilhos",
                column: "TrilhoID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasGuia_GuiaID",
                table: "ReservasGuia",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasGuia_TrilhoID",
                table: "ReservasGuia",
                column: "TrilhoID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasGuia_TuristaID",
                table: "ReservasGuia",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAvaliacao_GuiaID",
                table: "RespostasAvaliacao",
                column: "GuiaID");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAvaliacao_OpcaoID",
                table: "RespostasAvaliacao",
                column: "OpcaoID");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAvaliacao_QuestaoID",
                table: "RespostasAvaliacao",
                column: "QuestaoID");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasAvaliacao_TuristaID",
                table: "RespostasAvaliacao",
                column: "TuristaID");

            migrationBuilder.CreateIndex(
                name: "IX_Trilhos_DificuldadeID",
                table: "Trilhos",
                column: "DificuldadeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadoTrilhos");

            migrationBuilder.DropTable(
                name: "ReservasGuia");

            migrationBuilder.DropTable(
                name: "RespostasAvaliacao");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Trilhos");

            migrationBuilder.DropTable(
                name: "Trilhos2");

            migrationBuilder.DropTable(
                name: "Guias");

            migrationBuilder.DropTable(
                name: "Opcoes");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "Turistas");

            migrationBuilder.DropTable(
                name: "Dificuldades");
        }
    }
}
