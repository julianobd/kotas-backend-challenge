using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents the evolution details of a Pokémon, including its name, order, and whether it is legendary or mythical.
/// </summary>
public class PokemonEvolution
{
    /// <summary>
    ///     Gets or sets the unique identifier for the Pokemon evolution.
    /// </summary>
    /// <value>
    ///     The unique identifier for the Pokemon evolution.
    /// </value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the Pokémon evolution.
    /// </summary>
    /// <value>
    ///     A string representing the name of the Pokémon evolution. The maximum length is 300 characters.
    /// </value>
    [MaxLength(300)]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the order of the Pokémon in its evolutionary line.
    ///     This property is optional and can have a value between 1 and 500.
    /// </summary>
    [Range(1, 500)]
    public int? Order { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the Pokémon is legendary.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the Pokémon is legendary; otherwise, <c>false</c>.
    /// </value>
    public bool IsLegendary { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the Pokémon is mythical.
    /// </summary>
    public bool IsMythical { get; set; }

    /// <summary>
    ///     Gets or sets the list of sprites associated with the Pokémon evolution.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonSprite" /> objects representing the sprites for this Pokémon evolution.
    /// </value>
    public virtual List<PokemonSprite> Sprites { get; set; } = [];

    /// <summary>
    ///     Gets or sets the captured Pokémon associated with this evolution.
    /// </summary>
    /// <remarks>
    ///     This property represents the captured Pokémon that has undergone this specific evolution.
    /// </remarks>
    public virtual CapturedPokemon? CapturedPokemon { get; set; } = null!;
}