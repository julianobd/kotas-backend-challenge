using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Dto
{
    /// <summary>
    /// Represents the evolution chain of a Pokémon, including details such as its ID, name, order, and whether it is legendary or mythical.
    /// </summary>
    public class PokemonEvolutionChainDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Order { get; set; }
        public bool IsLegendary { get; set; }
        public bool IsMythical { get; set; }
        public List<String> Sprites { get; set; } = [];
    }
}
