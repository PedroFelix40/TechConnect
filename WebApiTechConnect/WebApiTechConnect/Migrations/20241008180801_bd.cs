using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTechConnect.Migrations
{
    /// <inheritdoc />
    public partial class bd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Midia",
                columns: table => new
                {
                    IdMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlMidia = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    BlobStorageName = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midia", x => x.IdMidia);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    GoogleId = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IdMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Midia_IdMidia",
                        column: x => x.IdMidia,
                        principalTable: "Midia",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.IdChat);
                    table.ForeignKey(
                        name: "FK_Chat_Usuarios_IdUsuario1",
                        column: x => x.IdUsuario1,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Chat_Usuarios_IdUsuario2",
                        column: x => x.IdUsuario2,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Publicacoes",
                columns: table => new
                {
                    IdPublicacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "DATE", nullable: false),
                    IdMidia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicacoes", x => x.IdPublicacao);
                    table.ForeignKey(
                        name: "FK_Publicacoes_Midia_IdMidia",
                        column: x => x.IdMidia,
                        principalTable: "Midia",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publicacoes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Seguidores",
                columns: table => new
                {
                    IdSeguidores = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSeguidor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSeguido = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguidores", x => x.IdSeguidores);
                    table.ForeignKey(
                        name: "FK_Seguidores_Usuarios_IdSeguido",
                        column: x => x.IdSeguido,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seguidores_Usuarios_IdSeguidor",
                        column: x => x.IdSeguidor,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    IdChatMessage = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    DataHoraEnvio = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdChat = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRemetente = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.IdChatMessage);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Chat_IdChat",
                        column: x => x.IdChat,
                        principalTable: "Chat",
                        principalColumn: "IdChat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Usuarios_IdRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    IdComentario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: false),
                    DataPublicacao = table.Column<DateTime>(type: "DATE", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPublicacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.IdComentario);
                    table.ForeignKey(
                        name: "FK_Comentarios_Publicacoes_IdPublicacao",
                        column: x => x.IdPublicacao,
                        principalTable: "Publicacoes",
                        principalColumn: "IdPublicacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Curtidas",
                columns: table => new
                {
                    IdCurtida = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPublicacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curtidas", x => x.IdCurtida);
                    table.ForeignKey(
                        name: "FK_Curtidas_Publicacoes_IdPublicacao",
                        column: x => x.IdPublicacao,
                        principalTable: "Publicacoes",
                        principalColumn: "IdPublicacao",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Curtidas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chat_IdUsuario1",
                table: "Chat",
                column: "IdUsuario1");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_IdUsuario2",
                table: "Chat",
                column: "IdUsuario2");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_IdChat",
                table: "ChatMessage",
                column: "IdChat");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_IdRemetente",
                table: "ChatMessage",
                column: "IdRemetente");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdPublicacao",
                table: "Comentarios",
                column: "IdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdPublicacao",
                table: "Curtidas",
                column: "IdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_IdUsuario",
                table: "Curtidas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacoes_IdMidia",
                table: "Publicacoes",
                column: "IdMidia");

            migrationBuilder.CreateIndex(
                name: "IX_Publicacoes_IdUsuario",
                table: "Publicacoes",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Seguidores_IdSeguido",
                table: "Seguidores",
                column: "IdSeguido");

            migrationBuilder.CreateIndex(
                name: "IX_Seguidores_IdSeguidor",
                table: "Seguidores",
                column: "IdSeguidor");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdMidia",
                table: "Usuarios",
                column: "IdMidia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Curtidas");

            migrationBuilder.DropTable(
                name: "Seguidores");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Publicacoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Midia");
        }
    }
}
