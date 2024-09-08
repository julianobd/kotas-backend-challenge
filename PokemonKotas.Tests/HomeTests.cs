using Bunit;
using Moq;
using PokemonKotas.Domain.Dto;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Web.Pages;
using PokemonKotas.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Blazored.SessionStorage;

namespace PokemonKotas.Tests
{
    public class HomeTests : TestContext
    {
        [Fact]
        public void HomeComponent_ShouldRenderCorrectly()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            var sessionStorageMock = new Mock<ISessionStorageService>();
            var sessionService = new SessionService(sessionStorageMock.Object);
            var httpClientMock = new Mock<HttpClient>();
            var webClientService = new WebClientService(httpClientMock.Object);

            Services.AddSingleton(pokemonServiceMock.Object);
            Services.AddSingleton(sessionService);
            Services.AddSingleton(webClientService);

            // Act
            var cut = RenderComponent<Home>();

            // Assert
            var header = cut.Find("h1.text-center.my-4");
            Assert.NotNull(header);
            Assert.Equal("Pokédex", header.TextContent);
        }

        [Fact]
        public void HomeComponent_ShouldFilterPokemons()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            var sessionStorageMock = new Mock<ISessionStorageService>();
            var sessionService = new SessionService(sessionStorageMock.Object);
            var httpClientMock = new Mock<HttpClient>();
            var webClientService = new WebClientService(httpClientMock.Object);

            var pokemons = new List<PokemonDto>
            {
                new PokemonDto { Name = "Pikachu", Sprites = [] },
                new PokemonDto { Name = "Charmander" , Sprites = []}
            };

            pokemonServiceMock.Setup(service => service.GetAllPokemons()).ReturnsAsync(pokemons);

            Services.AddSingleton(pokemonServiceMock.Object);
            Services.AddSingleton(sessionService);
            Services.AddSingleton(webClientService);

            var cut = RenderComponent<Home>();

            // Act
            cut.Find("input").Change("Pikachu");
            cut.Find("button").Click();

            // Assert
            Assert.Contains("Pikachu", cut.Markup);
            Assert.DoesNotContain("Charmander", cut.Markup);
        }

        [Fact]
        public void HomeComponent_ShouldPaginatePokemons()
        {
            // Arrange
            var pokemonServiceMock = new Mock<IPokemonService>();
            var sessionStorageMock = new Mock<ISessionStorageService>();
            var sessionService = new SessionService(sessionStorageMock.Object);
            var httpClientMock = new Mock<HttpClient>();
            var webClientService = new WebClientService(httpClientMock.Object);

            var pokemons = Enumerable.Range(1, 30).Select(i => new PokemonDto { Name = $"Pokemon{i}", Sprites = [] }).ToList();

            pokemonServiceMock.Setup(service => service.GetAllPokemons()).ReturnsAsync(pokemons);

            Services.AddSingleton(pokemonServiceMock.Object);
            Services.AddSingleton(sessionService);
            Services.AddSingleton(webClientService);

            var cut = RenderComponent<Home>();

            // Act
            cut.Find("#NextPage").Click();

            // Assert
            Assert.Contains("Pokemon21", cut.Markup);
            Assert.DoesNotContain("Pokemon1", cut.Markup);
        }
    }
}
