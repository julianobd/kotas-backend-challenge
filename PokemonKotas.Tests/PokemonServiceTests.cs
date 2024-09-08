using Moq;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Infra.Services;
using PokemonKotas.Infra;
using StrawberryShake;
using System;

namespace PokemonKotas.Tests;

public class PokemonServiceTests
{
    private readonly Mock<IPokemonClient> _pokemonClientMock;
    private readonly Mock<ICacheService<IOperationResult<IGetAllPokemonsResult>, IPokemonClient>> _cacheServiceMock;
    private readonly PokemonService _pokemonService;

    public PokemonServiceTests()
    {
        _pokemonClientMock = new Mock<IPokemonClient>();
        _cacheServiceMock = new Mock<ICacheService<IOperationResult<IGetAllPokemonsResult>, IPokemonClient>>();
        _pokemonService = new PokemonService(_pokemonClientMock.Object, _cacheServiceMock.Object);
    }

    [Fact]
    public async Task GetAllPokemons_ShouldReturnAllPokemons()
    {
        // Arrange
        var pokemons = new List<IGetAllPokemons_Pokemon_v2_pokemon>
        {
            new MockPokemon { Id = 1, Name = "Pikachu" },
            new MockPokemon { Id = 2, Name = "Bulbasaur" }
        };

        var mockResult = new Mock<IGetAllPokemonsResult>();
        mockResult.Setup(x => x.Pokemon_v2_pokemon)
            .Returns(pokemons);

        _cacheServiceMock.Setup(x => x.GetPokemons(_pokemonClientMock.Object))
            .ReturnsAsync(new OperationResult<IGetAllPokemonsResult>(mockResult.Object, null, null, null));

        // Act
        var result = await _pokemonService.GetAllPokemons();

        // Assert
        Assert.Equal(pokemons.Count, result.Count());
    }

    [Fact]
    public async Task GetRandomPokemon_ShouldReturnOnePokemons()
    {
        // Arrange
        var pokemons = new List<IGetAllPokemons_Pokemon_v2_pokemon>
        {
            new MockPokemon { Id = 1, Name = "Pikachu" },
        };

        var mockResult = new Mock<IGetAllPokemonsResult>();
        mockResult.Setup(x => x.Pokemon_v2_pokemon)
            .Returns(pokemons);

        _cacheServiceMock.Setup(x => x.GetPokemons(_pokemonClientMock.Object))
            .ReturnsAsync(new OperationResult<IGetAllPokemonsResult>(mockResult.Object, null, null, null));

        // Act
        var result = await _pokemonService.GetRandomPokemons(1);

        // Assert
        Assert.Equal(result.Count(), pokemons.Count);
    }

    [Fact]
    public async Task GetRandomPokemon_ShouldNotReturnPokemons()
    {
        // Arrange
        var pokemons = new List<IGetAllPokemons_Pokemon_v2_pokemon>
        {
        };

        var mockResult = new Mock<IGetAllPokemonsResult>();
        mockResult.Setup(x => x.Pokemon_v2_pokemon)
            .Returns(pokemons);

        _cacheServiceMock.Setup(x => x.GetPokemons(_pokemonClientMock.Object))
            .ReturnsAsync(new OperationResult<IGetAllPokemonsResult>(mockResult.Object, null, null, null));

        // Act
        var result = await _pokemonService.GetRandomPokemons(5);

        // Assert
        Assert.Empty(result);
    }


    [Fact]
    public async Task GetPokemonById_ShouldReturnOnePokemons()
    {
        // Arrange
        var pokemons = new List<IGetAllPokemons_Pokemon_v2_pokemon>
        {
            new MockPokemon { Id = 2, Name = "Bulbasaur" },
        };

        var mockResult = new Mock<IGetAllPokemonsResult>();
        mockResult.Setup(x => x.Pokemon_v2_pokemon)
            .Returns(pokemons);

        _cacheServiceMock.Setup(x => x.GetPokemons(_pokemonClientMock.Object))
            .ReturnsAsync(new OperationResult<IGetAllPokemonsResult>(mockResult.Object, null, null, null));

        // Act
        var result = await _pokemonService.GetPokemonByIdAsync(2);

        // Assert
        Assert.Equal(result.Id, pokemons.FirstOrDefault().Id);
    }
}

// Classe de mock que implementa a interface IGetAllPokemons_Pokemon_v2_pokemon
public class MockPokemon : IGetAllPokemons_Pokemon_v2_pokemon
{
    public int Id { get; set; } = 1;
    public string Name { get; set; }
    public IReadOnlyList<IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonabilities> Pokemon_v2_pokemonabilities => new List<MockAbility>() { new MockAbility(), new MockAbility() };
    public IReadOnlyList<IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonsprites> Pokemon_v2_pokemonsprites => new List<SpriteMain>() { new SpriteMain(), new SpriteMain(), new SpriteMain() };
    public IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy Pokemon_v2_pokemonspecy => new MockPokemonSpecy();
}

public class SpriteMain : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonsprites
{
    public string? Sprites { get; } = Random.Shared.Next(1, 999).ToString();
}

public class MockAbility : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonabilities
{
    public IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonabilities_Pokemon_v2_ability Pokemon_v2_ability => new MockAbility2();

}

public class MockAbility2 : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonabilities_Pokemon_v2_ability
{
    public int Id { get; } = Random.Shared.Next(1, 151);
    public string Name { get; } = Random.Shared.Next(1, 151).ToString();
}

public class MockPokemonSpecy : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy
{
    public IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain? Pokemon_v2_evolutionchain => new MockEvolutionChain(1);

}

public class MockEvolutionChain(int? id = null) : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain
{
    public int Id { get; } = id ?? Random.Shared.Next(1, 151);

    public IReadOnlyList<
            IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies>
        Pokemon_v2_pokemonspecies => new List<MockPokemonSpecies>()
    {
        new MockPokemonSpecies(1),
        new MockPokemonSpecies(2),
        new MockPokemonSpecies(),
        new MockPokemonSpecies(),
        new MockPokemonSpecies(),
        new MockPokemonSpecies(),
        new MockPokemonSpecies(),
    };


}

public class MockPokemonSpecies(int? id = null) : IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies
{
    public int? Order { get; } = Random.Shared.Next(1, 5);
    public bool Is_legendary { get; set; } = true;
    public bool Is_mythical { get; set; } = false;

    public IReadOnlyList<
            IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies_Pokemon_v2_pokemons>
        Pokemon_v2_pokemons => new List<SpecieEvolution>() { new SpecieEvolution(), new SpecieEvolution(), new SpecieEvolution() };

    public int? Evolves_from_species_id { get; } = Random.Shared.Next(1, 151);
    public int Id { get; set; } = id ?? Random.Shared.Next(1, 151);
    public string Name { get; } = Random.Shared.Next(1, 151).ToString();
}

public class SpecieEvolution :
    IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies_Pokemon_v2_pokemons
{
    public IReadOnlyList<
            IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies_Pokemon_v2_pokemons_Pokemon_v2_pokemonsprites>
        Pokemon_v2_pokemonsprites
        => new List<SpritesFromEvolution>() { new SpritesFromEvolution() };
}

public class SpritesFromEvolution :
    IGetAllPokemons_Pokemon_v2_pokemon_Pokemon_v2_pokemonspecy_Pokemon_v2_evolutionchain_Pokemon_v2_pokemonspecies_Pokemon_v2_pokemons_Pokemon_v2_pokemonsprites
{
    public string? Sprites { get; set; } = Random.Shared.Next(1, 999).ToString();
}

