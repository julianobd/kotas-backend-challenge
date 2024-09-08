using PokemonKotas.Data.Models;

namespace PokemonKotas.Data.Repositories;

/// <summary>
/// </summary>
public interface IMasterPokemonRepository
{
    /// <summary>
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<MasterPokemon?> GetMasterPokemonByIdAsync(int Id);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task<List<MasterPokemon>?> GetAllRanking();

    /// <summary>
    /// </summary>
    /// <param name="masterPokemon"></param>
    /// <returns></returns>
    Task<int> AddMasterPokemonAsync(MasterPokemon masterPokemon);

    /// <summary>
    ///     Adds a captured Pokémon to the specified master Pokémon's collection.
    /// </summary>
    /// <param name="masterPokemonId">The ID of the master Pokémon to which the captured Pokémon will be added.</param>
    /// <param name="pokemon">The captured Pokémon to be added.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a boolean value indicating whether
    ///     the operation was successful.
    /// </returns>
    Task<bool> AddCapturedPokemon(int masterPokemonId, CapturedPokemon pokemon);

    /// <summary>
    ///     Retrieves a list of all master Pokémon trainers from the database.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="MasterPokemon" /> objects, or <c>null</c> if no master Pokémon trainers are found.
    /// </returns>
    Task<List<MasterPokemon>?> GetAllMasters();

    /// <summary>
    ///     Asynchronously clears all data from the database, including master Pokémon, captured Pokémon,
    ///     Pokémon abilities, Pokémon evolutions, and Pokémon sprites.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Clear();
}