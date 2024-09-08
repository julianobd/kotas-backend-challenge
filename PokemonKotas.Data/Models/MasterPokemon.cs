using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonKotas.Data.Models;

/// <summary>
///     Represents a master Pokémon trainer who can capture and manage multiple Pokémon.
/// </summary>
public class MasterPokemon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(300)] public string Name { get; set; }

    [Range(1, 150)] public int Age { get; set; }

    public DateTime RegisterDateTime { get; set; }

    public virtual List<CapturedPokemon> CapturedPokemons { get; set; } = [];
}