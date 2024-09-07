using PokemonKotas.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Interfaces
{
    public interface IPokemonService
    {
        /// <summary>
        /// Retrieves a Pokémon by its unique identifier.
        /// </summary>
        /// <param name="pokemonId">The unique identifier of the Pokémon.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Pokémon data transfer object if found; otherwise, null.</returns>
        ValueTask<PokemonDto?> GetPokemonByIdAsync(int pokemonId);

        /// <summary>
        /// Retrieves a specified amount of random Pokémon data.
        /// </summary>
        /// <param name="ammount">The number of random Pokémon to retrieve.</param>
        /// <returns>A collection of <see cref="PokemonDto"/> representing the random Pokémon.</returns>
        ValueTask<IEnumerable<PokemonDto>> GetRandomPokemons(int ammount);

        /// <summary>
        /// Retrieves all Pokémon data.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see cref="PokemonDto"/> representing all Pokémon.</returns>
        ValueTask<IEnumerable<PokemonDto>> GetAllPokemons();
    }
}
