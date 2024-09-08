namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents a Data Transfer Object (DTO) for a Pokémon.
/// </summary>
public class PokemonDto
{
    /// <summary>
    ///     Gets or sets the unique identifier for the Pokémon.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the Pokémon.
    /// </summary>
    /// <value>
    ///     A string representing the name of the Pokémon.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the date when the Pokémon was captured.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime" /> representing the capture date of the Pokémon.
    /// </value>
    public DateTime CapturedDate { get; set; }

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
    /// <value>
    ///     <c>true</c> if the Pokémon is mythical; otherwise, <c>false</c>.
    /// </value>
    public bool IsMythical { get; set; }

    /// <summary>
    ///     Gets or sets the list of sprite URLs or base64-encoded images for the Pokémon.
    /// </summary>
    public List<string> Sprites { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the list of abilities associated with the Pokémon.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonAbilityDto" /> objects representing the abilities of the Pokémon.
    /// </value>
    public List<PokemonAbilityDto> Abilities { get; set; } = [];

    /// <summary>
    ///     Gets or sets the evolution chain of the Pokémon.
    /// </summary>
    /// <value>
    ///     A list of <see cref="PokemonEvolutionChainDto" /> representing the evolution stages of the Pokémon.
    /// </value>
    public List<PokemonEvolutionChainDto> EvolutionChain { get; set; } = [];
}