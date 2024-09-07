using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Data.Models
{
    /// <summary>
    /// Represents a captured Pokémon with its associated details such as name, capture date, sprites, abilities, evolutions, and the master Pokémon who captured it.
    /// </summary>
    public class CapturedPokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CapturedDate { get; set; }
        [MaxLength(300)] 
        public string PokemonName { get; set; } = null!;
        
        public List<PokemonSprite> Sprites { get; set; }
        public List<PokemonAbility> Abilities { get; set; }
        public List<PokemonEvolution> Evolutions { get; set; }
        public virtual MasterPokemon? MasterPokemon { get; set; } = null!;
    }
}
