using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cep = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Logradouro = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Complemento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Unidade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Localidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Estado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Regiao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Ddd = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoaFisica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Sobrenome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaFisica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoaJuridica",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RazaoSocial = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaJuridica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaJuridica_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaFisicaEndereco",
                columns: table => new
                {
                    PessoaFisicaId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaFisicaEndereco", x => new { x.PessoaFisicaId, x.EnderecoId });
                    table.ForeignKey(
                        name: "FK_PessoaFisicaEndereco_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PessoaFisicaEndereco_PessoaFisica_PessoaFisicaId",
                        column: x => x.PessoaFisicaId,
                        principalTable: "PessoaFisica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaFisica_Cpf",
                table: "PessoaFisica",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaFisicaEndereco_EnderecoId",
                table: "PessoaFisicaEndereco",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_Cnpj",
                table: "PessoaJuridica",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_EnderecoId",
                table: "PessoaJuridica",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_NomeFantasia",
                table: "PessoaJuridica",
                column: "NomeFantasia");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaJuridica_RazaoSocial",
                table: "PessoaJuridica",
                column: "RazaoSocial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaFisicaEndereco");

            migrationBuilder.DropTable(
                name: "PessoaJuridica");

            migrationBuilder.DropTable(
                name: "PessoaFisica");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
