using PokemonKotas.Domain.Dto;

namespace PokemonKotas.Domain.Interfaces;

public interface IMasterPokemonService
{
    Task<MasterPokemonDto> GetMasterById(int id);
    Task<int> AddMasterPokemonAsync(MasterPokemonDto masterPokemon);
    Task<List<MasterPokemonDto>?> GetAllMasters();
    Task<List<MasterRankDto>?> GetRanking(int ammount = 10);
    Task<bool> AddCapturedPokemonAsync(int masterPokemonId, PokemonDto pokemon);
    Task Clear();
}