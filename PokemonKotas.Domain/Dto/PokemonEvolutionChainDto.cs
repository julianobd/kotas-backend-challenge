namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents the evolution chain of a Pokémon, including details such as its ID, name, order, and whether it is
///     legendary or mythical.
/// </summary>
public class PokemonEvolutionChainDto
{
    /// <summary>
    ///     Gets or sets the unique identifier for the Pokémon evolution chain.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the Pokémon in the evolution chain.
    /// </summary>
    /// <value>
    ///     The name of the Pokémon.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the order of the Pokémon in its evolution chain.
    /// </summary>
    /// <remarks>
    ///     The order property indicates the position of the Pokémon within its evolution chain.
    ///     It is nullable to account for Pokémon that may not have a defined order.
    /// </remarks>
    public int? Order { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the Pokémon in the evolution chain is legendary.
    /// </summary>
    public bool IsLegendary { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the Pokémon is mythical.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the Pokémon is mythical; otherwise, <c>false</c>.
    /// </value>
    public bool IsMythical { get; set; }

    /// <summary>
    ///     Gets or sets the list of sprite URLs associated with the Pokémon in the evolution chain.
    /// </summary>
    public List<string> Sprites { get; set; } = [];
}