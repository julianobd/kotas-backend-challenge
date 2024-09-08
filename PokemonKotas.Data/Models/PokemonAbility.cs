using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents an ability that a Pokémon can have, including its name and association with a captured Pokémon.
/// </summary>
public class PokemonAbility
{
    /// <summary>
    ///     Gets or sets the unique identifier for the Pokemon ability.
    /// </summary>
    /// <value>
    ///     The unique identifier for the Pokemon ability.
    /// </value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the Pokémon ability.
    /// </summary>
    /// <value>
    ///     The name of the Pokémon ability, which is a string with a maximum length of 300 characters.
    /// </value>
    [MaxLength(300)]
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the captured Pokémon associated with this ability.
    /// </summary>
    /// <remarks>
    ///     This property establishes a relationship between the Pokémon ability and the captured Pokémon that possesses this
    ///     ability.
    /// </remarks>
    public virtual CapturedPokemon? CapturedPokemon { get; set; } = null!;
}