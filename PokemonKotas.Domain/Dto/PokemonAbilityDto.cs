namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents a Data Transfer Object (DTO) for a Pokémon's ability.
/// </summary>
public class PokemonAbilityDto
{
    /// <summary>
    ///     Gets or sets the unique identifier for the Pokémon ability.
    /// </summary>
    /// <value>
    ///     The unique identifier for the Pokémon ability.
    /// </value>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the Pokémon ability.
    /// </summary>
    /// <value>
    ///     A string representing the name of the Pokémon ability.
    /// </value>
    public string Name { get; set; } = null!;
}