using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Age = table.Column<int>(type: "INTEGER", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPokemons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CapturedPokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CapturedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PokemonName = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    MasterPokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    MasterPokemonId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapturedPokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId",
                        column: x => x.MasterPokemonId,
                        principalTable: "MasterPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId1",
                        column: x => x.MasterPokemonId1,
                        principalTable: "MasterPokemons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PokemoEvolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Order = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: true),
                    IsLegendary = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsMythical = table.Column<bool>(type: "INTEGER", nullable: false),
                    CapturedPokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemoEvolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemoEvolutions_CapturedPokemons_CapturedPokemonId",
                        column: x => x.CapturedPokemonId,
                        principalTable: "CapturedPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    CapturedPokemonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAbilities_CapturedPokemons_CapturedPokemonId",
                        column: x => x.CapturedPokemonId,
                        principalTable: "CapturedPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokemonSprite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 10, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpriteUrl = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    SpriteBase64 = table.Column<string>(type: "TEXT", nullable: false),
                    CapturedPokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    PokemonEvolutionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonSprite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSprite_CapturedPokemons_CapturedPokemonId",
                        column: x => x.CapturedPokemonId,
                        principalTable: "CapturedPokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonSprite_PokemoEvolutions_PokemonEvolutionId",
                        column: x => x.PokemonEvolutionId,
                        principalTable: "PokemoEvolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapturedPokemons_MasterPokemonId",
                table: "CapturedPokemons",
                column: "MasterPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_CapturedPokemons_MasterPokemonId1",
                table: "CapturedPokemons",
                column: "MasterPokemonId1");

            migrationBuilder.CreateIndex(
                name: "IX_PokemoEvolutions_CapturedPokemonId",
                table: "PokemoEvolutions",
                column: "CapturedPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_CapturedPokemonId",
                table: "PokemonAbilities",
                column: "CapturedPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSprite_CapturedPokemonId",
                table: "PokemonSprite",
                column: "CapturedPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSprite_PokemonEvolutionId",
                table: "PokemonSprite",
                column: "PokemonEvolutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonSprite");

            migrationBuilder.DropTable(
                name: "PokemoEvolutions");

            migrationBuilder.DropTable(
                name: "CapturedPokemons");

            migrationBuilder.DropTable(
                name: "MasterPokemons");
        }
    }
}
