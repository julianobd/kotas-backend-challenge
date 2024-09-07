using PokemonKotas.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Interfaces
{
    public interface IMasterPokemonService
    {
        Task<MasterPokemonDto> GetMasterById(int id);
        Task<int> AddMasterPokemonAsync(MasterPokemonDto masterPokemon);
        Task<List<MasterPokemonDto>?> GetAllMasters();
        Task<bool> AddCapturedPokemonAsync(int masterPokemonId, PokemonDto pokemon);
        Task Clear();
    }
}
