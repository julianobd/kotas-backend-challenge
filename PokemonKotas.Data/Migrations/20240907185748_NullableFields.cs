using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonKotas.Data.Migrations
{
    /// <inheritdoc />
    public partial class NullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId",
                table: "CapturedPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemoEvolutions_CapturedPokemons_CapturedPokemonId",
                table: "PokemoEvolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_CapturedPokemons_CapturedPokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSprite_CapturedPokemons_CapturedPokemonId",
                table: "PokemonSprite");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSprite_PokemoEvolutions_PokemonEvolutionId",
                table: "PokemonSprite");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonEvolutionId",
                table: "PokemonSprite",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemonSprite",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemonAbilities",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemoEvolutions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "MasterPokemonId",
                table: "CapturedPokemons",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId",
                table: "CapturedPokemons",
                column: "MasterPokemonId",
                principalTable: "MasterPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemoEvolutions_CapturedPokemons_CapturedPokemonId",
                table: "PokemoEvolutions",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_CapturedPokemons_CapturedPokemonId",
                table: "PokemonAbilities",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSprite_CapturedPokemons_CapturedPokemonId",
                table: "PokemonSprite",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSprite_PokemoEvolutions_PokemonEvolutionId",
                table: "PokemonSprite",
                column: "PokemonEvolutionId",
                principalTable: "PokemoEvolutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId",
                table: "CapturedPokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemoEvolutions_CapturedPokemons_CapturedPokemonId",
                table: "PokemoEvolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_CapturedPokemons_CapturedPokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSprite_CapturedPokemons_CapturedPokemonId",
                table: "PokemonSprite");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonSprite_PokemoEvolutions_PokemonEvolutionId",
                table: "PokemonSprite");

            migrationBuilder.AlterColumn<int>(
                name: "PokemonEvolutionId",
                table: "PokemonSprite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemonSprite",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemonAbilities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CapturedPokemonId",
                table: "PokemoEvolutions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MasterPokemonId",
                table: "CapturedPokemons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CapturedPokemons_MasterPokemons_MasterPokemonId",
                table: "CapturedPokemons",
                column: "MasterPokemonId",
                principalTable: "MasterPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemoEvolutions_CapturedPokemons_CapturedPokemonId",
                table: "PokemoEvolutions",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_CapturedPokemons_CapturedPokemonId",
                table: "PokemonAbilities",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSprite_CapturedPokemons_CapturedPokemonId",
                table: "PokemonSprite",
                column: "CapturedPokemonId",
                principalTable: "CapturedPokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonSprite_PokemoEvolutions_PokemonEvolutionId",
                table: "PokemonSprite",
                column: "PokemonEvolutionId",
                principalTable: "PokemoEvolutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
