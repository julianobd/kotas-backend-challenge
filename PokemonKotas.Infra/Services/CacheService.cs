using Microsoft.Extensions.Caching.Memory;
using PokemonKotas.Domain.Interfaces;
using StrawberryShake;

namespace PokemonKotas.Infra.Services;

/// <summary>
///     Provides caching services for retrieving and storing data.
/// </summary>
/// <typeparam name="TOut">The type of the data to be retrieved from the cache.</typeparam>
/// <typeparam name="TIn">The type of the client used to fetch data if it is not found in the cache.</typeparam>
/// <param name="cache">The memory cache instance used for storing and retrieving cached data.</param>
public class CacheService<TOut, TIn>(IMemoryCache cache) : ICacheService<TOut, TIn>
{
    /// <summary>
    ///     Retrieves a collection of data from the cache or fetches them from the provided client if not cached.
    /// </summary>
    /// <param name="pokemonClient">The client used to fetch data if they are not found in the cache.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of data.</returns>
    public async ValueTask<TOut?> GetPokemons(TIn pokemonClient)
    {
        if (pokemonClient is null) throw new ArgumentNullException(nameof(pokemonClient));
        if (cache.TryGetValue<IOperationResult<IGetAllPokemonsResult>>("pokemons",
                out var resultCache)) return (TOut)resultCache!;
        var result = await ((IPokemonClient)pokemonClient)!.GetAllPokemons.ExecuteAsync();
        result.EnsureNoErrors();
        cache.Set("pokemons", result, TimeSpan.FromMinutes(30));
        return (TOut)result!;
    }
}