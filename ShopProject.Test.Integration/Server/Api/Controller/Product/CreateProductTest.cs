using FluentAssertions;
using ShopProject.Test.Integration.Helper;
using ShopProjectWebServer.Api.Common;
using ShopProjectWebServer.Api.DtoModels.Product;
using ShopProjectWebServer.Service.Modules.Setting.Interface;
using ShopProjectWebServer.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Test.Integration.Server.Api.Controller.Product
{
    public class CreateProductTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CreateProductTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "sUgZTOoMA2awUBcT");
        }

        [Fact]
        public async Task AddProduct_Should_Create_Product_WhenProductIsValid()
        {
            // Arrange
            var product = new CreateProductDto
            {
                NameProduct = "Test",
                Code = "04",
                Price = 100,
                Count = 5,
                Articule = "A1",
                Unit_ID = 1,
                CodeUKTZED_ID = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Product/Add",
                product);

            // Assert
            var content = await response.Content.ReadAsStringAsync();




            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<ProductDto>>();

            var res = new OperationResult<ProductDto>()
            {
                Data = result.Data,
                ErrorMessage = result.Error,
                ErrorType = (ShopProjectWebServer.Services.Common.Enum.ErrorType)result.ErrorType,
                Source = (ShopProjectWebServer.Services.Common.Enum.ErrorSource)result.Source,
                Status = (ShopProjectWebServer.Services.Common.Enum.ResultStatus)result.Status,
                ValidationErrors = result.Errors,
            };

            res.IsSuccess.Should().BeTrue();
            res.Data.NameProduct.Should().Be("Test");
            res.Data.Code.Should().Be("04");
        }
        [Fact]
        public async Task AddProduct_Should_Create_Product_WhenProductIsNotValid()
        {
            // Arrange
            var product = new CreateProductDto
            {
                NameProduct = "",
                Code = "",
                Price = 100,
                Count = 5,
                Articule = "A1",
                Unit_ID = 1,
                CodeUKTZED_ID = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Product/Add",
                product);

            // Assert
            var content = await response.Content.ReadAsStringAsync();




            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<ProductDto>>();

            var res = new OperationResult<ProductDto>()
            {
                Data = result.Data,
                ErrorMessage = result.Error,
                ErrorType = (ShopProjectWebServer.Services.Common.Enum.ErrorType)result.ErrorType,
                Source = (ShopProjectWebServer.Services.Common.Enum.ErrorSource)result.Source,
                Status = (ShopProjectWebServer.Services.Common.Enum.ResultStatus)result.Status,
                ValidationErrors = result.Errors,
            };

            res.IsError.Should().BeTrue();
            res.ValidationErrors.ElementAt(0).Should().Be("Ведіть назву товару");
            res.ValidationErrors.ElementAt(1).Should().Be("Ведіть штрихкод товару");
            res.ErrorType.Should().Be(ShopProjectWebServer.Services.Common.Enum.ErrorType.Validation);
            res.Source.Should().Be(ShopProjectWebServer.Services.Common.Enum.ErrorSource.Client);
        }
        [Fact]
        public async Task AddProduct_Should_Create_Product_WhenProductIsExists()
        {
            // Arrange
            var product = new CreateProductDto
            {
                NameProduct = "Test",
                Code = "3215465874561",
                Price = 100,
                Count = 5,
                Articule = "A1",
                Unit_ID = 1,
                CodeUKTZED_ID = 1,
                Status = 1,
                CreatedAt = DateTime.Now,
            };

            // Act
            var response = await _client.PostAsJsonAsync(
                "/api/Product/Add",
                product);

            // Assert
            var content = await response.Content.ReadAsStringAsync();




            var result = await response.Content
                .ReadFromJsonAsync<ApiResponse<ProductDto>>();


            var res = new OperationResult<ProductDto>()
            {
                Data = result.Data,
                ErrorMessage = result.Error, 
                ErrorType = (ShopProjectWebServer.Services.Common.Enum.ErrorType)result.ErrorType,
                Source = (ShopProjectWebServer.Services.Common.Enum.ErrorSource)result.Source,
                Status = (ShopProjectWebServer.Services.Common.Enum.ResultStatus)result.Status,
                ValidationErrors = result.Errors,
            };

            res.IsError.Should().BeTrue();
            res.ErrorMessage.Should().Be("Товар існує");
            res.ErrorType.Should().Be(ShopProjectWebServer.Services.Common.Enum.ErrorType.ObjectExists);
            res.Source.Should().Be(ShopProjectWebServer.Services.Common.Enum.ErrorSource.Database);
        }
    }
}
