using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Domain.Dto
{
    public class MasterPokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime? RegisterDateTime { get; set; }
        public List<PokemonDto> CapturedPokemons { get; set; } = new ();
    }
}
