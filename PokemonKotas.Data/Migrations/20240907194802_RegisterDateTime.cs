using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class RegisterDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDateTime",
                table: "MasterPokemons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDateTime",
                table: "MasterPokemons");
        }
    }
}
