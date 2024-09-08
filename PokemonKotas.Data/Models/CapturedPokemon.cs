using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents a captured Pokémon with its associated details such as name, capture date, sprites, abilities,
///     evolutions, and the master Pokémon who captured it.
/// </summary>
public class CapturedPokemon
{
    /// <summary>
    ///     Gets or sets the unique identifier for the captured Pokémon.
    /// </summary>
    /// <value>
    ///     The unique identifier for the captured Pokémon.
    /// </value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the date and time when the Pokémon was captured.
    /// </summary>
    public DateTime CapturedDate { get; set; }

    /// <summary>
    ///     Gets or sets the name of the captured Pokémon.
    /// </summary>
    /// <remarks>
    ///     The name is limited to a maximum length of 300 characters.
    /// </remarks>
    [MaxLength(300)]
    public string PokemonName { get; set; } = null!;

    /// <summary>
    ///     Gets or sets a value indicating whether the captured Pokémon is legendary.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the Pokémon is legendary; otherwise, <c>false</c>.
    /// </value>
    public bool IsLegendary { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the captured Pokémon is mythical.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the Pokémon is mythical; otherwise, <c>false</c>.
    /// </value>
    public bool IsMythical { get; set; }

    /// <summary>
    ///     Gets or sets the list of sprites associated with the captured Pokémon.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonSprite" /> objects representing the visual representations of the captured Pokémon.
    /// </value>
    public List<PokemonSprite> Sprites { get; set; }

    /// <summary>
    ///     Gets or sets the list of abilities that the captured Pokémon possesses.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonAbility" /> objects representing the abilities of the captured Pokémon.
    /// </value>
    public List<PokemonAbility> Abilities { get; set; }

    /// <summary>
    ///     Gets or sets the list of evolutions for the captured Pokémon.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonEvolution" /> objects representing the evolutions of the captured Pokémon.
    /// </value>
    public List<PokemonEvolution> Evolutions { get; set; }

    /// <summary>
    ///     Gets or sets the master Pokémon trainer who captured this Pokémon.
    /// </summary>
    /// <remarks>
    ///     This property establishes a relationship between the captured Pokémon and the master Pokémon trainer.
    /// </remarks>
    public virtual MasterPokemon? MasterPokemon { get; set; } = null!;
}