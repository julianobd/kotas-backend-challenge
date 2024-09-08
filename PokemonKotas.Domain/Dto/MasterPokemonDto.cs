namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents a master Pokemon data transfer object (DTO).
///     This class contains information about a master Pokemon, including its ID, name, age, registration date, and a list
///     of captured Pokemons.
/// </summary>
public class MasterPokemonDto
{
    /// <summary>
    ///     Gets or sets the unique identifier for the master Pokémon.
    /// </summary>
    /// <value>
    ///     The unique identifier for the master Pokémon.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the master Pokémon trainer.
    /// </summary>
    /// <value>
    ///     The name of the master Pokémon trainer.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    ///     Gets or sets the age of the master Pokémon trainer.
    /// </summary>
    /// <value>
    ///     The age of the master Pokémon trainer.
    /// </value>
    public int Age { get; set; }

    /// <summary>
    ///     Gets or sets the date and time when the master Pokemon was registered.
    /// </summary>
    /// <value>
    ///     The registration date and time of the master Pokemon. This value is nullable.
    /// </value>
    public DateTime? RegisterDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the list of captured Pokémon.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonDto" /> representing the captured Pokémon.
    /// </value>
    public List<PokemonDto> CapturedPokemons { get; set; } = new();
}