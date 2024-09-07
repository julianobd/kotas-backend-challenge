using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationshipOfCapturedPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId1",
                table: "CapturedPokemons");

            migrationBuilder.DropIndex(
                name: "IX_CapturedPokemons_MasterPokemonId1",
                table: "CapturedPokemons");

            migrationBuilder.DropColumn(
                name: "MasterPokemonId1",
                table: "CapturedPokemons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MasterPokemonId1",
                table: "CapturedPokemons",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CapturedPokemons_MasterPokemonId1",
                table: "CapturedPokemons",
                column: "MasterPokemonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId1",
                table: "CapturedPokemons",
                column: "MasterPokemonId1",
                principalTable: "MasterPokemons",
                principalColumn: "Id");
        }
    }
}
