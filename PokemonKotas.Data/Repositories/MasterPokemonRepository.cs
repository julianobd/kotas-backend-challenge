using Microsoft.EntityFrameworkCore;
using PokemonKotas.Data.Context;
using PokemonKotas.Data.Models;

namespace PokemonKotas.Data.Repositories;

/// <summary>
/// </summary>
/// <param name="dbContext"></param>
public class MasterPokemonRepository(MasterPokemonDbContext dbContext) : IMasterPokemonRepository
{
    /// <summary>
    ///     Asynchronously retrieves a master Pokémon by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the master Pokémon.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the master Pokémon if found;
    ///     otherwise, null.
    /// </returns>
    public async Task<MasterPokemon?> GetMasterPokemonByIdAsync(int Id)
    {
        return await dbContext.MasterPokemons.AsNoTracking()
            .Include(x => x.CapturedPokemons).ThenInclude(x => x.Sprites)
            .Include(x => x.CapturedPokemons).ThenInclude(x => x.Abilities)
            .Include(x => x.CapturedPokemons).ThenInclude(x => x.Evolutions).ThenInclude(x => x.Sprites)
            .Where(x => x.Id == Id).FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Asynchronously retrieves a list of all master Pokémon along with their captured Pokémon.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of all master Pokémon with
    ///     their captured Pokémon, or null if no master Pokémon are found.
    /// </returns>
    public async Task<List<MasterPokemon>?> GetAllRanking()
    {
        return await dbContext.MasterPokemons.AsNoTracking()
            .Include(x => x.CapturedPokemons)
            .ToListAsync();
    }

    /// <summary>
    ///     Adds a new master Pokémon to the database asynchronously.
    /// </summary>
    /// <param name="masterPokemon">The master Pokémon entity to be added.</param>
    /// <returns>The ID of the added master Pokémon if the operation is successful; otherwise, 0.</returns>
    public async Task<int> AddMasterPokemonAsync(MasterPokemon masterPokemon)
    {
        dbContext.MasterPokemons.Add(masterPokemon);
        var success = await dbContext.SaveChangesAsync() > 0;
        if (success)
            return masterPokemon.Id;
        return 0;
    }

    /// <summary>
    ///     Adds a captured Pokémon to the specified master Pokémon's collection.
    /// </summary>
    /// <param name="masterPokemonId">The ID of the master Pokémon to which the captured Pokémon will be added.</param>
    /// <param name="pokemon">The captured Pokémon to be added.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a boolean value indicating whether
    ///     the operation was successful.
    /// </returns>
    public async Task<bool> AddCapturedPokemon(int masterPokemonId, CapturedPokemon pokemon)
    {
        var masterPokemon = await dbContext.MasterPokemons.Include(x => x.CapturedPokemons)
            .Where(x => x.Id == masterPokemonId).FirstOrDefaultAsync();
        if (masterPokemon == null) return false;
        masterPokemon.CapturedPokemons.Add(pokemon);
        return await dbContext.SaveChangesAsync() > 0;
    }

    /// <summary>
    ///     Retrieves a list of all master Pokémon trainers from the database.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="MasterPokemon" /> objects, or <c>null</c> if no master Pokémon trainers are found.
    /// </returns>
    public async Task<List<MasterPokemon>?> GetAllMasters()
    {
        return await dbContext.MasterPokemons.AsNoTracking().ToListAsync();
    }

    /// <summary>
    ///     Asynchronously clears all data from the database, including master Pokémon, captured Pokémon,
    ///     Pokémon abilities, Pokémon evolutions, and Pokémon sprites.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Clear()
    {
        dbContext.PokemonSprite.RemoveRange(dbContext.PokemonSprite);
        dbContext.PokemonAbilities.RemoveRange(dbContext.PokemonAbilities);
        dbContext.PokemoEvolutions.RemoveRange(dbContext.PokemoEvolutions);
        dbContext.CapturedPokemons.RemoveRange(dbContext.CapturedPokemons);
        dbContext.MasterPokemons.RemoveRange(dbContext.MasterPokemons);
        await dbContext.SaveChangesAsync();
    }
}