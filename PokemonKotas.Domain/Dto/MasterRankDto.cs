namespace PokemonKotas.Domain.Dto;

public class MasterRankDto
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public int CapturedPokemons { get; set; }
    public int LegendaryPokemons { get; set; }
    public int MythicalPokemons { get; set; }
    public int NormalPokemons { get; set; }
    public decimal Score { get; set; }
}