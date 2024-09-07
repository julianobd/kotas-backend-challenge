using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PokemonKotas.Data.Models;
using PokemonKotas.Data.Repositories;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Infra.Helper;

namespace PokemonKotas.Infra.Services
{
    public class MasterPokemonService(MasterPokemonRepository masterPokemonRepository, HttpClient httpClient) : IMasterPokemonService
    {
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
                                    new PokemonAbilityDto { Id = a.Id, Name = a.Name })
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

        public async Task<bool> AddCapturedPokemonAsync(int masterPokemonId, PokemonDto pokemon)
        {
            await DownloadSpriteAsBase64UpdatingPokemonDto(pokemon);
            var capturedPokemon = new CapturedPokemon
            {
                PokemonName = pokemon.Name,
                CapturedDate = pokemon.CapturedDate,
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

        public async Task Clear()
        {
            await masterPokemonRepository.Clear();
        }

        private async Task DownloadSpriteAsBase64UpdatingPokemonDto(List<PokemonDto> pokemons)
        {
            foreach (var pokemon in pokemons)
            {
                await DownloadSpriteAsBase64UpdatingPokemonDto(pokemon);
            }
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
            {
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
    }
}
