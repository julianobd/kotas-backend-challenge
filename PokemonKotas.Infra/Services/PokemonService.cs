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
    public class PokemonService(IPokemonClient pokemonClient, ICacheService<StrawberryShake.IOperationResult<IGetPokemonsResult>, IPokemonClient> cacheService) : IPokemonService
    {
        /// <summary>
        /// Retrieves a Pokémon by its unique identifier.
        /// </summary>
        /// <param name="pokemonId">The unique identifier of the Pokémon.</param>
        /// <returns>The ValueTask result contains the Pokémon data transfer object if found; otherwise, null.</returns>
        public async ValueTask<PokemonDto?> GetPokemonByIdAsync(int pokemonId)
        {
            var pokemons = await cacheService.GetPokemons(pokemonClient);
            var pokemonDto = pokemons.Data.Pokemon_v2_pokemon.Where(x => x.Id == pokemonId).Select(x => new PokemonDto()
            {
                Id = x.Id,
                Name = x.Name,
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
                            .Select(z => z.Sprites)
                    })
            }).FirstOrDefault();
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
            var pokemonDto = pokemons.Data.Pokemon_v2_pokemon.Select(x => new PokemonDto()
            {
                Id = x.Id,
                Name = x.Name,
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
                            .Select(z => z.Sprites)
                    })
            }).OrderBy(x => Guid.NewGuid()).Take(ammount);
            return pokemonDto;
        }
    }
}