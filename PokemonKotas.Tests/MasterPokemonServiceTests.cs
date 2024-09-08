using Moq;
using PokemonKotas.Data.Models;
using PokemonKotas.Data.Repositories;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Infra.Services;

namespace PokemonKotas.Tests;

public class MasterPokemonServiceTests
{
    private readonly Mock<HttpClient> _httpClientMock;
    private readonly Mock<IMasterPokemonRepository> _masterPokemonRepositoryMock;
    private readonly MasterPokemonService _masterPokemonService;

    public MasterPokemonServiceTests()
    {
        // Mock do repositório e HttpClient
        _masterPokemonRepositoryMock = new Mock<IMasterPokemonRepository>();
        _httpClientMock = new Mock<HttpClient>();

        // Instancia o serviço com mocks
        _masterPokemonService =
            new MasterPokemonService(_masterPokemonRepositoryMock.Object, _httpClientMock.Object);
    }

    [Fact]
    public async Task GetMasterById_ShouldReturnCorrectMasterPokemon()
    {
        // Arrange
        var masterPokemon = new MasterPokemon
        {
            Id = 1,
            Name = "Ash Ketchum",
            Age = 10,
            CapturedPokemons = new List<CapturedPokemon>
            {
                new()
                {
                    PokemonName = "Pikachu", IsLegendary = false, Sprites = [], IsMythical = true,
                    CapturedDate = DateTime.Now, Evolutions = [],
                    Abilities = new List<PokemonAbility>
                        { new() { Id = Random.Shared.Next(1, 999), Name = Random.Shared.Next(1, 999).ToString() } }
                }
            }
        };

        _masterPokemonRepositoryMock
            .Setup(repo => repo.GetMasterPokemonByIdAsync(1))
            .ReturnsAsync(masterPokemon);

        // Act
        var result = await _masterPokemonService.GetMasterById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ash Ketchum", result.Name);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task AddMasterPokemon_ShouldAddNewMasterPokemon()
    {
        // Arrange
        var masterPokemonDto = new MasterPokemonDto
        {
            Name = "Misty",
            Age = 15,
            CapturedPokemons = new List<PokemonDto>
            {
                new() { Name = "Starmie", IsLegendary = false, Sprites = [] }
            }
        };

        _masterPokemonRepositoryMock
            .Setup(repo => repo.AddMasterPokemonAsync(It.IsAny<MasterPokemon>()))
            .ReturnsAsync(1);

        // Act
        var result = await _masterPokemonService.AddMasterPokemonAsync(masterPokemonDto);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetRanking_ShouldReturnTopMasters()
    {
        // Arrange
        var masterPokemonList = new List<MasterPokemon>
        {
            new()
            {
                Name = "Ash", Age = 10, CapturedPokemons = new List<CapturedPokemon>
                    { new() { PokemonName = "Pikachu", IsLegendary = false, Sprites = [] } }
            },
            new()
            {
                Name = "Brock", Age = 16, CapturedPokemons = new List<CapturedPokemon>
                    { new() { PokemonName = "Onix", IsLegendary = true, Sprites = [] } }
            },
            new()
            {
                Name = "Blopt", Age = 16, CapturedPokemons = new List<CapturedPokemon>
                    { new() { PokemonName = "Mew", IsMythical = true, Sprites = [] } }
            }
        };

        _masterPokemonRepositoryMock
            .Setup(repo => repo.GetAllRanking())
            .ReturnsAsync(masterPokemonList);

        // Act
        var result = await _masterPokemonService.GetRanking(3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal("Blopt", result.First().Name);
        Assert.Equal("Brock", result.Skip(1).Take(1).FirstOrDefault()?.Name);
        Assert.Equal("Ash", result.Skip(2).Take(1).FirstOrDefault()?.Name);
    }

    [Fact]
    public async Task Clear_ShouldCallRepositoryClear()
    {
        // Act
        await _masterPokemonService.Clear();

        // Assert
        _masterPokemonRepositoryMock.Verify(repo => repo.Clear(), Times.Once);
    }
}