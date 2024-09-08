using PokemonKotas.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Data.Repositories
{
    public interface IMasterPokemonRepository
    {
        Task<MasterPokemon?> GetMasterPokemonByIdAsync(int Id);
        Task<List<MasterPokemon>?> GetAllRanking();
        Task<int> AddMasterPokemonAsync(MasterPokemon masterPokemon);
        Task<bool> AddCapturedPokemon(int masterPokemonId, CapturedPokemon pokemon);
        Task<List<MasterPokemon>?> GetAllMasters();
        Task Clear();
    }
}
