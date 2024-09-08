using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents a Pokémon sprite, including its URL and Base64 encoded image data.
///     This class also maintains relationships with captured Pokémon and Pokémon evolutions.
/// </summary>
public class PokemonSprite
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string SpriteBase64 { get; set; }

    public virtual CapturedPokemon? CapturedPokemon { get; set; } = null!;
    public virtual PokemonEvolution? PokemonEvolution { get; set; } = null!;
}