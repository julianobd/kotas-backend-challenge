namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents the data transfer object for a master rank.
/// </summary>
public class MasterRankDto
{
    /// <summary>
    ///     Gets or sets the name of the master rank.
    /// </summary>
    /// <value>
    ///     The name of the master rank.
    /// </value>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the age of the master rank.
    /// </summary>
    /// <value>
    ///     The age of the master rank.
    /// </value>
    public int Age { get; set; }

    /// <summary>
    ///     Gets or sets the number of Pokémon captured by the master.
    /// </summary>
    public int CapturedPokemons { get; set; }

    /// <summary>
    ///     Gets or sets the number of legendary Pokémon captured by the master.
    /// </summary>
    public int LegendaryPokemons { get; set; }

    /// <summary>
    ///     Gets or sets the number of mythical Pokémon captured by the master.
    /// </summary>
    public int MythicalPokemons { get; set; }

    /// <summary>
    ///     Gets or sets the number of normal Pokémon captured by the master.
    ///     Normal Pokémon are those that are neither legendary nor mythical.
    /// </summary>
    public int NormalPokemons { get; set; }

    /// <summary>
    ///     Gets or sets the score of the master rank, which is calculated based on the number and types of captured Pokémon.
    /// </summary>
    /// <value>
    ///     A decimal value representing the score of the master rank.
    /// </value>
    public decimal Score { get; set; }
}