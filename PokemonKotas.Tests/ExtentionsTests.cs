using PokemonKotas.Infra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonKotas.Tests
{
    public class ExtentionsTests
    {
        [Fact]
        public void ToBase64_String_ShouldReturnBase64EncodedString()
        {
            // Arrange
            var input = "Hello, World!";
            var expected = "SGVsbG8sIFdvcmxkIQ==";

            // Act
            var result = input.ToBase64();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToBase64_ByteArray_ShouldReturnBase64EncodedString()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111 };
            var expected = "SGVsbG8=";

            // Act
            var result = input.ToBase64();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ResetIds_ShouldResetIdsToZero()
        {
            // Arrange
            var testObject = new TestClass
            {
                Id = 1,
                Name = "Test",
                NestedObject = new NestedClass
                {
                    Id = 2,
                    Description = "Nested Test"
                },
                NestedList = new List<NestedClass>
                {
                    new NestedClass { Id = 3, Description = "List Item 1" },
                    new NestedClass { Id = 4, Description = "List Item 2" }
                }
            };

            // Act
            testObject.ResetIds();

            // Assert
            Assert.Equal(0, testObject.Id);
            Assert.Equal(0, testObject.NestedObject.Id);
            Assert.All(testObject.NestedList, item => Assert.Equal(0, item.Id));
        }

        private class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public NestedClass NestedObject { get; set; }
            public List<NestedClass> NestedList { get; set; }
        }

        private class NestedClass
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
    }
}
