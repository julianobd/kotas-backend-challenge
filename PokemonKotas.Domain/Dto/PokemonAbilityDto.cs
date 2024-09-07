using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Dto
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for a Pokémon's ability.
    /// </summary>
    public class PokemonAbilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
