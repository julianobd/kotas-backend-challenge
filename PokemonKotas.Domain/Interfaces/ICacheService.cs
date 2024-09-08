namespace PokemonKotas.Domain.Interfaces;

/// <summary>
///     Defines a service for caching and retrieving data.
/// </summary>
/// <typeparam name="TOut">The type of the data to be retrieved from the cache.</typeparam>
/// <typeparam name="TIn">The type of the client used to fetch data if it is not found in the cache.</typeparam>
public interface ICacheService<TOut, in TIn>
{
    /// <summary>
    ///     Retrieves a collection of data from the cache or fetches them from the provided client if not cached.
    /// </summary>
    /// <param name="pokemonClient">The client used to fetch data if they are not found in the cache.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of data.</returns>
    ValueTask<TOut?> GetPokemons(TIn pokemonClient);
}