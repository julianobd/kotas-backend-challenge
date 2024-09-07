using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUrlField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpriteUrl",
                table: "PokemonSprite");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpriteUrl",
                table: "PokemonSprite",
                type: "TEXT",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");
        }
    }
}
