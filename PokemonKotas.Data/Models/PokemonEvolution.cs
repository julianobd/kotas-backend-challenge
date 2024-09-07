using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonKotas.Data.Models
{
    /// <summary>
    /// Represents the evolution details of a Pokémon, including its name, order, and whether it is legendary or mythical.
    /// </summary>
    public class PokemonEvolution
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; } = null!;
        [Range(1, 500)]
        public int? Order { get; set; }
        public bool IsLegendary { get; set; }
        public bool IsMythical { get; set; }
        
        public virtual List<PokemonSprite> Sprites { get; set; } = [];
        public virtual CapturedPokemon? CapturedPokemon { get; set; } = null!;
    }
}
