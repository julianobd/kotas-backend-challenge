using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingLegendaryMythicalToCapturedPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLegendary",
                table: "CapturedPokemons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMythical",
                table: "CapturedPokemons",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLegendary",
                table: "CapturedPokemons");

            migrationBuilder.DropColumn(
                name: "IsMythical",
                table: "CapturedPokemons");
        }
    }
}
