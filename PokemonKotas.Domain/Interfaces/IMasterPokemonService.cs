using PokemonKotas.Domain.Dto;

namespace PokemonKotas.Domain.Interfaces;

/// <summary>
///     Provides methods for managing master Pokémon entities, including retrieval, addition, ranking, and clearing
///     operations.
/// </summary>
public interface IMasterPokemonService
{
    /// <summary>
    ///     Retrieves a master Pokémon entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the master Pokémon.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     <see cref="MasterPokemonDto" /> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<MasterPokemonDto> GetMasterById(int id);

    /// <summary>
    ///     Adds a new master Pokémon to the repository asynchronously.
    /// </summary>
    /// <param name="masterPokemon">
    ///     The master Pokémon data transfer object (DTO) containing the details of the master Pokémon
    ///     to be added.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the unique identifier of the newly
    ///     added master Pokémon.
    /// </returns>
    Task<int> AddMasterPokemonAsync(MasterPokemonDto masterPokemon);

    /// <summary>
    ///     Retrieves all master Pokémon entities from the repository asynchronously.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="MasterPokemonDto" /> objects if found; otherwise, <c>null</c>.
    /// </returns>
    Task<List<MasterPokemonDto>?> GetAllMasters();

    /// <summary>
    ///     Retrieves the ranking of master Pokémon entities based on their scores.
    /// </summary>
    /// <param name="ammount">The number of top-ranked master Pokémon entities to retrieve. Defaults to 10 if not specified.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="MasterRankDto" /> objects representing the top-ranked master Pokémon entities if found; otherwise,
    ///     <c>null</c>.
    /// </returns>
    Task<List<MasterRankDto>?> GetRanking(int ammount = 10);

    /// <summary>
    ///     Adds a captured Pokémon to the specified master Pokémon asynchronously.
    /// </summary>
    /// <param name="masterPokemonId">The unique identifier of the master Pokémon to which the captured Pokémon will be added.</param>
    /// <param name="pokemon">The data transfer object (DTO) containing the details of the captured Pokémon.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result indicates whether the captured Pokémon was
    ///     successfully added.
    /// </returns>
    Task<bool> AddCapturedPokemonAsync(int masterPokemonId, PokemonDto pokemon);

    /// <summary>
    ///     Clears all master Pokémon entities from the repository asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Clear();
}