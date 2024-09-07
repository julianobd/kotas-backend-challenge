using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Dto
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a Pokémon.
    /// </summary>
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CapturedDate { get; set; }
        public List<string> Sprites { get; set; } = null!;
        public List<PokemonAbilityDto> Abilities { get; set; } = [];
        public List<PokemonEvolutionChainDto> EvolutionChain { get; set; } = [];
    }



}
