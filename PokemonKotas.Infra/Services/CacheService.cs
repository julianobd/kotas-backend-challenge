using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PokemonKotas.Domain.Interfaces;
using StrawberryShake;

namespace PokemonKotas.Infra.Services
{
    /// <summary>
    /// Defines a service for caching and retrieving data.
    /// </summary>
    /// <typeparam name="TOut">The type of the data to be retrieved from the cache.</typeparam>
    /// <typeparam name="TIn">The type of the client used to fetch data if it is not found in the cache.</typeparam>
    public class CacheService<TOut, TIn> : ICacheService<TOut, TIn>
    {
        private static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());

        /// <summary>
        /// Retrieves a collection of data from the cache or fetches them from the provided client if not cached.
        /// </summary>
        /// <param name="pokemonClient">The client used to fetch data if they are not found in the cache.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of data.</returns>
        public async ValueTask<TOut?> GetPokemons(TIn pokemonClient)
        {
            if (pokemonClient is null) throw new ArgumentNullException(nameof(pokemonClient));
            if (Cache.TryGetValue<StrawberryShake.IOperationResult<IGetPokemonsResult>>("pokemons", 
                    out var resultCache)) return (TOut)resultCache!;
            var result = await  (((IPokemonClient)pokemonClient)!).GetPokemons.ExecuteAsync();
            Cache.Set("pokemons", result, TimeSpan.FromMinutes(30));
            return (TOut)result!;
        }
    }
}
