using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonKotas.Data.Context;
using PokemonKotas.Data.Models;

namespace PokemonKotas.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    public class MasterPokemonRepository(MasterPokemonDbContext dbContext)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<MasterPokemon?> GetMasterPokemonByIdAsync(int Id)
        {
            return await dbContext.MasterPokemons.AsNoTracking()
                .Include(x => x.CapturedPokemons).ThenInclude(x => x.Sprites)
                .Include(x => x.CapturedPokemons).ThenInclude(x => x.Abilities)
                .Include(x => x.CapturedPokemons).ThenInclude(x => x.Evolutions).ThenInclude(x => x.Sprites)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        
        public async Task<int> AddMasterPokemonAsync(MasterPokemon masterPokemon)
        {
            dbContext.MasterPokemons.Add(masterPokemon);
            var success = await dbContext.SaveChangesAsync() > 0;
            if (success)
                return masterPokemon.Id;
            return 0;
        }

        public async Task<bool> AddCapturedPokemon(int masterPokemonId, CapturedPokemon pokemon)
        {
            var masterPokemon = await dbContext.MasterPokemons.Include(x => x.CapturedPokemons).Where(x => x.Id == masterPokemonId).FirstOrDefaultAsync();
            if (masterPokemon == null) return false;
            masterPokemon.CapturedPokemons.Add(pokemon);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<MasterPokemon>?> GetAllMasters()
        {
            return await dbContext.MasterPokemons.AsNoTracking().ToListAsync();
        }

        public async Task Clear()
        {
            dbContext.PokemonSprite.RemoveRange(dbContext.PokemonSprite);
            dbContext.PokemonAbilities.RemoveRange(dbContext.PokemonAbilities);
            dbContext.PokemoEvolutions.RemoveRange(dbContext.PokemoEvolutions);
            dbContext.CapturedPokemons.RemoveRange(dbContext.CapturedPokemons);
            dbContext.MasterPokemons.RemoveRange(dbContext.MasterPokemons);
            /*
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM PokemonSprite");
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM PokemonAbilities");
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM PokemonEvolutions");
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM CapturedPokemon");
            await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM MasterPokemon");*/
            await dbContext.SaveChangesAsync();
        }
    }
}
