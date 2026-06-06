using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Api.Validation.Interface;
using ShopProjectWebServer.Api.Validation.ProductValidation; 
using FluentAssertions;
using Xunit;

namespace ShopProject.Tests.Unit.Server.Api.Validation
{
    public class CreateProductValidationTest
    {
        [Fact]
        public void Validation_ShouldReturnNoErrors_WhenModelIsValid()
        {
            var service = new CreateProductValidator();

            var model = new CreateProductDto
            {
                NameProduct = "Test",
                Code = "123",
                Price = 100,
                Count = 5,
                Articule = "A1"
            };
             
            var result = service.Validation(model); 
            result.Errors.Should().BeEmpty(); 
        }

        [Fact]
        public void Validation_ShouldReturnErrors_WhenModelIsEmpty()
        { 
            var service = new CreateProductValidator();

            var model = new CreateProductDto
            {
                NameProduct = "",
                Code = "",
                Price = 0,
                Count = 0,
                Articule = ""
            };
             
            var result = service.Validation(model);
             
            result.Errors.Should().HaveCount(5);
        }
        [Theory]
        [InlineData("", "Ведіть назву товару")]
        [InlineData(null, "Ведіть назву товару")]
        public void Validation_ShouldReturnNameError(string value, string expectedError)
        {
            var service = new CreateProductValidator();

            var model = new CreateProductDto
            {
                NameProduct = "",
                Code = "123",
                Price = 100,
                Count = 1,
                Articule = "A1"
            };

            var result = service.Validation(model);

            Assert.Contains(expectedError, result.Errors);
        }
    }
}
