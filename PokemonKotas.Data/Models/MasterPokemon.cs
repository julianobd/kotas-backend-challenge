using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents a master Pokémon trainer who can capture and manage multiple Pokémon.
/// </summary>
public class MasterPokemon
{
    /// <summary>
    ///     Gets or sets the unique identifier for the master Pokémon trainer.
    /// </summary>
    /// <value>
    ///     The unique identifier for the master Pokémon trainer.
    /// </value>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the master Pokémon trainer.
    /// </summary>
    /// <value>
    ///     A string representing the name of the master Pokémon trainer. The maximum length is 300 characters.
    /// </value>
    [MaxLength(300)]
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the age of the Master Pokemon.
    /// </summary>
    /// <value>
    ///     The age must be between 1 and 150.
    /// </value>
    [Range(1, 150)]
    public int Age { get; set; }

    /// <summary>
    ///     Gets or sets the date and time when the master Pokémon trainer was registered.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime" /> representing the registration date and time.
    /// </value>
    public DateTime RegisterDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the list of Pokémon captured by the master Pokémon trainer.
    /// </summary>
    /// <value>
    ///     A list of <see cref="CapturedPokemon" /> objects representing the Pokémon captured by the trainer.
    /// </value>
    public virtual List<CapturedPokemon> CapturedPokemons { get; set; } = [];
}