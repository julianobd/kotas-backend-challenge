namespace PokemonKotas.Domain.Dto;

/// <summary>
///     Represents a Data Transfer Object (DTO) for a Pokémon's ability.
/// </summary>
public class PokemonAbilityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}