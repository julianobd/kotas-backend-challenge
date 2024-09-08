using System.Text.Json;
using PokemonKotas.Data.Models;
using PokemonKotas.Data.Repositories;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;

namespace PokemonKotas.Infra.Services;

/// <summary>
///     Service class responsible for managing master Pokémon entities.
/// </summary>
/// <param name="masterPokemonRepository">
///     Repository for accessing master Pokémon data.
/// </param>
/// <param name="httpClient">
///     HTTP client for making external API calls.
/// </param>
public class MasterPokemonService(IMasterPokemonRepository masterPokemonRepository, HttpClient httpClient)
    : IMasterPokemonService
{
    /// <summary>
    ///     Retrieves a master Pokémon by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the master Pokémon.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     <see cref="MasterPokemonDto" /> if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="id" /> is less than or equal to zero.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while retrieving the master Pokémon.</exception>
    public async Task<MasterPokemonDto> GetMasterById(int id)
    {
        var masterPokemon = await masterPokemonRepository.GetMasterPokemonByIdAsync(id);
        if (masterPokemon == null) return null;

        var masterPokemonDto = new MasterPokemonDto
        {
            Id = masterPokemon.Id,
            Name = masterPokemon.Name,
            Age = masterPokemon.Age,
            RegisterDateTime = masterPokemon.RegisterDateTime,
            CapturedPokemons = masterPokemon.CapturedPokemons.Select(p => new PokemonDto
                {
                    Id = p.Id,
                    Name = p.PokemonName,
                    CapturedDate = p.CapturedDate,
                    Sprites = p.Sprites.Select(s => s.SpriteBase64).ToList(),
                    Abilities =
                        p.Abilities.Select(a =>
                                new PokemonAbilityDto { Id = a.Id, Name = a.Name! })
                            .ToList(),
                    EvolutionChain = p.Evolutions.Select(e => new PokemonEvolutionChainDto
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Sprites = e.Sprites.Select(s => s.SpriteBase64).ToList()
                        })
                        .ToList()
                })
                .ToList()
        };

        return masterPokemonDto;
    }

    /// <summary>
    ///     Adds a new master Pokémon asynchronously.
    /// </summary>
    /// <param name="masterPokemon">
    ///     The master Pokémon data transfer object containing details of the master Pokémon to be
    ///     added.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the identifier of the added master
    ///     Pokémon.
    /// </returns>
    public async Task<int> AddMasterPokemonAsync(MasterPokemonDto masterPokemon)
    {
        var json = JsonSerializer.Serialize(masterPokemon);
        await DownloadSpriteAsBase64UpdatingPokemonDto(masterPokemon.CapturedPokemons);
        var masterPokemonEntity = new MasterPokemon
        {
            Name = masterPokemon.Name,
            Age = masterPokemon.Age,
            RegisterDateTime = DateTime.Now,
            CapturedPokemons = masterPokemon.CapturedPokemons.Select(p => new CapturedPokemon
                {
                    PokemonName = p.Name,
                    CapturedDate = p.CapturedDate,
                    Sprites = p.Sprites.Select(s => new PokemonSprite { SpriteBase64 = s }).ToList(),
                    Abilities = p.Abilities.Select(a => new PokemonAbility { Id = a.Id, Name = a.Name }).ToList(),
                    Evolutions = p.EvolutionChain.Select(e => new PokemonEvolution
                        {
                            Id = e.Id,
                            Name = e.Name,
                            Sprites = e.Sprites.Select(s => new PokemonSprite { SpriteBase64 = s }).ToList()
                        })
                        .ToList()
                })
                .ToList()
        };
        var result = await masterPokemonRepository.AddMasterPokemonAsync(masterPokemonEntity);
        return result;
    }

    /// <summary>
    ///     Retrieves the ranking of master Pokémon trainers based on their captured Pokémon.
    /// </summary>
    /// <param name="ammount">
    ///     The number of top-ranked master Pokémon trainers to return. Defaults to 10.
    /// </param>
    /// <returns>
    ///     A list of <see cref="MasterRankDto" /> representing the top-ranked master Pokémon trainers, or null if no data is
    ///     available.
    /// </returns>
    /// <remarks>
    ///     The ranking is determined by calculating a score for each master Pokémon trainer based on the number and types of
    ///     Pokémon they have captured.
    /// </remarks>
    public async Task<List<MasterRankDto>?> GetRanking(int ammount = 10)
    {
        var allMasters = await masterPokemonRepository.GetAllRanking();
        if (allMasters == null || !allMasters.Any()) return null;

        var ranking = allMasters.Select(master => new MasterRankDto
            {
                Name = master.Name,
                Age = master.Age,
                CapturedPokemons = master.CapturedPokemons.Count,
                LegendaryPokemons = master.CapturedPokemons.Count(p => p.IsLegendary),
                MythicalPokemons = master.CapturedPokemons.Count(p => p.IsMythical),
                NormalPokemons = master.CapturedPokemons.Count(p => p is { IsLegendary: false, IsMythical: false }),
                Score = CalculateScore(master.CapturedPokemons)
            })
            .OrderByDescending(r => r.Score)
            .Take(ammount)
            .ToList();

        return ranking;
    }

    /// <summary>
    ///     Retrieves a list of all master Pokémon trainers.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="MasterPokemonDto" /> objects representing the master Pokémon trainers, or <c>null</c> if no trainers are
    ///     found.
    /// </returns>
    /// <remarks>
    ///     This method fetches all master Pokémon trainers from the repository and maps them to
    ///     <see cref="MasterPokemonDto" /> objects, including their captured Pokémon and related details.
    /// </remarks>
    public async Task<List<MasterPokemonDto>?> GetAllMasters()
    {
        var masterPokemons = await masterPokemonRepository.GetAllMasters();
        if (masterPokemons == null) return null;

        var masterPokemonDtos = masterPokemons.Select(masterPokemon => new MasterPokemonDto
        {
            Id = masterPokemon.Id,
            Name = masterPokemon.Name,
            Age = masterPokemon.Age,
            RegisterDateTime = masterPokemon.RegisterDateTime,
            CapturedPokemons = masterPokemon.CapturedPokemons.Select(p => new PokemonDto
            {
                Id = p.Id,
                Name = p.PokemonName,
                CapturedDate = p.CapturedDate,
                Sprites = p.Sprites.Select(s => s.SpriteBase64).ToList(),
                Abilities = p.Abilities.Select(a => new PokemonAbilityDto { Id = a.Id, Name = a.Name }).ToList(),
                EvolutionChain = p.Evolutions.Select(e => new PokemonEvolutionChainDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sprites = e.Sprites.Select(s => s.SpriteBase64).ToList()
                }).ToList()
            }).ToList()
        }).ToList();

        return masterPokemonDtos;
    }

    /// <summary>
    ///     Adds a captured Pokémon to the repository for a specific master Pokémon.
    /// </summary>
    /// <param name="masterPokemonId">The ID of the master Pokémon to which the captured Pokémon will be added.</param>
    /// <param name="pokemon">The details of the captured Pokémon to be added.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a boolean value indicating whether
    ///     the operation was successful.
    /// </returns>
    public async Task<bool> AddCapturedPokemonAsync(int masterPokemonId, PokemonDto pokemon)
    {
        await DownloadSpriteAsBase64UpdatingPokemonDto(pokemon);
        var capturedPokemon = new CapturedPokemon
        {
            PokemonName = pokemon.Name,
            CapturedDate = DateTime.Now,
            IsLegendary = pokemon.IsLegendary,
            IsMythical = pokemon.IsMythical,
            Sprites = pokemon.Sprites.Select(s => new PokemonSprite { SpriteBase64 = s }).ToList(),
            Abilities = pokemon.Abilities.Select(a => new PokemonAbility { Id = a.Id, Name = a.Name }).ToList(),
            Evolutions = pokemon.EvolutionChain.Select(e => new PokemonEvolution
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sprites = e.Sprites.Select(s => new PokemonSprite { SpriteBase64 = s }).ToList()
                })
                .ToList()
        };

        var result = await masterPokemonRepository.AddCapturedPokemon(masterPokemonId, capturedPokemon);
        return result;
    }

    /// <summary>
    ///     Clears all master Pokémon data from the repository.
    /// </summary>
    /// <returns>A task that represents the asynchronous clear operation.</returns>
    public async Task Clear()
    {
        await masterPokemonRepository.Clear();
    }

    private decimal CalculateScore(List<CapturedPokemon> capturedPokemons)
    {
        const decimal legendaryWeight = 2.0m;
        const decimal mythicalWeight = 3.0m;
        const decimal normalWeight = 1.0m;

        var score = capturedPokemons.Sum(p =>
            Math.Floor((p.IsLegendary ? legendaryWeight : 0) + (p.IsMythical ? mythicalWeight : 0) +
                       (p is { IsLegendary: false, IsMythical: false } ? normalWeight : 0)) * 100) / 100;
        return score;
    }

    private async Task DownloadSpriteAsBase64UpdatingPokemonDto(List<PokemonDto> pokemons)
    {
        foreach (var pokemon in pokemons) await DownloadSpriteAsBase64UpdatingPokemonDto(pokemon);
    }

    private async Task DownloadSpriteAsBase64UpdatingPokemonDto(PokemonDto pokemon)
    {
        foreach (var sprite in pokemon.Sprites.ToList())
        {
            if (string.IsNullOrEmpty(sprite)) continue;
            var result = await httpClient.GetByteArrayAsync(sprite);
            var base64String = Convert.ToBase64String(result);
            var index = pokemon.Sprites.IndexOf(sprite);
            pokemon.Sprites[index] = $"data:image/png;base64,{base64String}"; // Atualiza para base64
        }

        foreach (var evolution in pokemon.EvolutionChain)
        foreach (var sprite in evolution.Sprites.ToList())
        {
            if (string.IsNullOrEmpty(sprite)) continue;
            var result = await httpClient.GetByteArrayAsync(sprite);
            var base64String = Convert.ToBase64String(result);
            var index = evolution.Sprites.IndexOf(sprite);
            evolution.Sprites[index] = $"data:image/png;base64,{base64String}"; // Atualiza para base64
        }
    }
}