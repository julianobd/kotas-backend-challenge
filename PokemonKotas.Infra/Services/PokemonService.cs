using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;

namespace PokemonKotas.Infra.Services
{
    /// <summary>
    /// Provides services for retrieving Pokémon data.
    /// </summary>
    /// <param name="pokemonClient">The client used to fetch Pokémon data from the GraphQL API.</param>
    /// <param name="cacheService">The cache service used to store and retrieve cached Pokémon data.</param>
    public class PokemonService(IPokemonClient pokemonClient, ICacheService<StrawberryShake.IOperationResult<IGetAllPokemonsResult>, IPokemonClient> cacheService) : IPokemonService
    {
        /// <summary>
        /// Retrieves a Pokémon by its unique identifier.
        /// </summary>
        /// <param name="pokemonId">The unique identifier of the Pokémon.</param>
        /// <returns>The ValueTask result contains the Pokémon data transfer object if found; otherwise, null.</returns>
        public async ValueTask<PokemonDto?> GetPokemonByIdAsync(int pokemonId)
        {
            var pokemons = await RetrieveAllPokemons();
            var pokemonDto = pokemons.FirstOrDefault(x => x.Id == pokemonId);
            return pokemonDto;
        }

        /// <summary>
        /// Retrieves a specified amount of random Pokémon data.
        /// </summary>
        /// <param name="ammount">The number of random Pokémon to retrieve.</param>
        /// <returns>ValueTask containing a collection of <see cref="PokemonDto"/> representing the random Pokémon.</returns>
        public async ValueTask<IEnumerable<PokemonDto>> GetRandomPokemons(int ammount)
        {
            var pokemons = await cacheService.GetPokemons(pokemonClient);
            var pokemonDto = (await RetrieveAllPokemons()).OrderBy(x => Guid.NewGuid()).Take(ammount);
            return pokemonDto;
        }

        /// <summary>
        /// Asynchronously retrieves all Pokémon from the cache or the Pokémon client.
        /// </summary>
        /// <remarks>
        /// This method fetches Pokémon data, transforms it into a collection of <see cref="PokemonDto"/> objects,
        /// and orders them by their ID before returning the result.
        /// </remarks>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an enumerable collection of <see cref="PokemonDto"/> objects.
        /// </returns>
        public async ValueTask<IEnumerable<PokemonDto>> GetAllPokemons()
        {
            var pokemonDto = await RetrieveAllPokemons();
            return pokemonDto.ToList();
        }

        private async ValueTask<IEnumerable<PokemonDto>> RetrieveAllPokemons()
        {
            var pokemons = await cacheService.GetPokemons(pokemonClient);
            return pokemons.Data.Pokemon_v2_pokemon.Select(x => new PokemonDto()
            {
                Id = x.Id,
                Name = x.Name,
                Sprites = x.Pokemon_v2_pokemonsprites.Where(y => y.Sprites != null).Select(y => y.Sprites),
                Abilities = x.Pokemon_v2_pokemonabilities.Select(y => new PokemonAbilityDto()
                {
                    Id = y.Pokemon_v2_ability?.Id,
                    Name = y.Pokemon_v2_ability?.Name
                }),
                EvolutionChain = x.Pokemon_v2_pokemonspecy.Pokemon_v2_evolutionchain.Pokemon_v2_pokemonspecies.Select(
                    y => new PokemonEvolutionChainDto()
                    {
                        Id = y.Id,
                        IsLegendary = y.Is_legendary,
                        IsMythical = y.Is_mythical,
                        Name = y.Name,
                        Order = y.Order,
                        Sprites = y.Pokemon_v2_pokemons.SelectMany(z => z.Pokemon_v2_pokemonsprites)
                            .Where(z => z.Sprites != null)
                            .Select(z => z.Sprites)
                    })
            }).OrderBy(x => x.Id);
        }
    }
}