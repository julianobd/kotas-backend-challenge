using Microsoft.Extensions.Caching.Memory;
using Moq;
using PokemonKotas.Infra;
using PokemonKotas.Infra.Services;
using StrawberryShake;

namespace PokemonKotas.Tests;

public class CacheServiceTests
{
    private readonly CacheService<IOperationResult<IGetAllPokemonsResult>, IPokemonClient> _cacheService;
    private readonly Mock<IMemoryCache> _memoryCacheMock;
    private readonly Mock<IPokemonClient> _pokemonClientMock;

    public CacheServiceTests()
    {
        _memoryCacheMock = new Mock<IMemoryCache>();
        _pokemonClientMock = new Mock<IPokemonClient>();
        _cacheService =
            new CacheService<IOperationResult<IGetAllPokemonsResult>, IPokemonClient>(_memoryCacheMock.Object);
    }

    [Fact]
    public async Task GetPokemons_ShouldReturnFromCache_WhenDataIsCached()
    {
        // Arrange
        var cachedResult = Mock.Of<IOperationResult<IGetAllPokemonsResult>>();
        object cacheEntry = cachedResult;
        _memoryCacheMock
            .Setup(cache => cache.TryGetValue("pokemons", out cacheEntry!))
            .Returns(true);

        // Act
        var result = await _cacheService.GetPokemons(_pokemonClientMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cachedResult, result);
    }

    [Fact]
    public async Task GetPokemons_ShouldFetchFromClient_WhenDataIsNotCached()
    {
        // Arrange
        var cacheEntryMock = new Mock<ICacheEntry>();
        _memoryCacheMock
            .Setup(cache => cache.TryGetValue("pokemons", out It.Ref<object>.IsAny!))
            .Returns(false);
        _memoryCacheMock
            .Setup(cache => cache.CreateEntry(It.IsAny<object>()))
            .Returns(cacheEntryMock.Object);

        var fetchedResultMock = new Mock<IOperationResult<IGetAllPokemonsResult>>();
        fetchedResultMock.Setup(r => r.Data).Returns(Mock.Of<IGetAllPokemonsResult>());
        fetchedResultMock.Setup(r => r.Errors).Returns(Array.Empty<IClientError>());

        _pokemonClientMock
            .Setup(client => client.GetAllPokemons.ExecuteAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(fetchedResultMock.Object);

        // Act
        var result = await _cacheService.GetPokemons(_pokemonClientMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fetchedResultMock.Object, result);
        cacheEntryMock.VerifySet(entry => entry.Value = fetchedResultMock.Object, Times.Once);
    }

    [Fact]
    public async Task GetPokemons_ShouldThrowArgumentNullException_WhenClientIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _cacheService.GetPokemons(null!));
    }
}