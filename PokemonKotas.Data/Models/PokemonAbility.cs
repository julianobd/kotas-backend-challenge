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
    /// Represents an ability that a Pokémon can have, including its name and association with a captured Pokémon.
    /// </summary>
    public class PokemonAbility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(300)]
        public string? Name { get; set; }

        public virtual CapturedPokemon? CapturedPokemon { get; set; } = null!;
    }
}
