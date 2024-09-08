using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PokemonKotas.Domain.Interfaces;

namespace PokemonKotas.Tests
{
    public class WebClientServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IWebClientService> _webClientServiceMock;

        public WebClientServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _webClientServiceMock = new Mock<IWebClientService>();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnDeserializedResponse_WhenRequestIsSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            var expectedResponse = new { Name = "Test" };
            _webClientServiceMock.Setup(service => service.GetAsync<object>(url))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _webClientServiceMock.Object.GetAsync<object>(url);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Name, ((dynamic)result).Name);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnDefault_WhenRequestIsNotSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            _webClientServiceMock.Setup(service => service.GetAsync<object>(url))
                .ReturnsAsync((object)null);

            // Act
            var result = await _webClientServiceMock.Object.GetAsync<object>(url);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnDeserializedResponse_WhenRequestIsSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            var input = new { Name = "Test" };
            var expectedResponse = new { Success = true };
            _webClientServiceMock.Setup(service => service.PutAsync<object, object>(url, input))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _webClientServiceMock.Object.PutAsync<object, object>(url, input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Success, ((dynamic)result).Success);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnDefault_WhenRequestIsNotSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            var input = new { Name = "Test" };
            _webClientServiceMock.Setup(service => service.PutAsync<object, object>(url, input))
                .ReturnsAsync((object)null);

            // Act
            var result = await _webClientServiceMock.Object.PutAsync<object, object>(url, input);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnDeserializedResponse_WhenRequestIsSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            var input = new { Name = "Test" };
            var expectedResponse = new { Success = true };
            _webClientServiceMock.Setup(service => service.PostAsync<object, object>(url, input))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _webClientServiceMock.Object.PostAsync<object, object>(url, input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.Success, ((dynamic)result).Success);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnDefault_WhenRequestIsNotSuccessful()
        {
            // Arrange
            var url = "http://example.com";
            var input = new { Name = "Test" };
            _webClientServiceMock.Setup(service => service.PostAsync<object, object>(url, input))
                .ReturnsAsync((object)null);

            // Act
            var result = await _webClientServiceMock.Object.PostAsync<object, object>(url, input);

            // Assert
            Assert.Null(result);
        }
    }
}
