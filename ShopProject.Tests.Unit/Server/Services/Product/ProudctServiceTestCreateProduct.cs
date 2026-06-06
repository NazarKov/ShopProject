using FluentAssertions;
using Moq;
using ShopProjectDataBase.Entities;
using ShopProjectWebServer.DataBase;
using ShopProjectWebServer.DataBase.Interface;
using ShopProjectWebServer.DataBase.Interface.DataBaseInterface;
using ShopProjectWebServer.DataBase.Interface.EntityInterface;
using ShopProjectWebServer.Models.Domain.Product;
using ShopProjectWebServer.Services.Modules.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Tests.Unit.Server.Services.Product
{
    public class ProudctServiceTestCreateProduct
    {
        [Fact]
        public async Task AddAsync_Should_ReturnSuccess_When_ProductIsValid()
        {
            var productTableMock = new Mock<IProductTableAccess>();
            productTableMock
                .Setup(x => x.AddAsync(It.IsAny<ProductEntity>()))
                .ReturnsAsync(new ProductEntity
                {
                    NameProduct = "Test",
                    Code = "123",
                    Price = 100,
                    Count = 5,
                    Articule = "A1"
                });
            productTableMock
              .Setup(x => x.ExistsByBarCode("143"))
              .ReturnsAsync(false);
            var dbAccessMock = new Mock<IDataAccess>();
            dbAccessMock.Setup(x => x.ProductTable).Returns(productTableMock.Object);

            var dbServiceMock = new Mock<IDataBaseService>();
            dbServiceMock.Setup(x => x.DataBaseAccess).Returns(dbAccessMock.Object);

            var product = new ShopProjectWebServer.Models.Domain.Product.Product
            {
                NameProduct = "Test",
                Code = "123",
                Price = 100,
                Count = 5,
                Articule = "A1"
            };
            var service = new ProductService(dbServiceMock.Object);

            var result = await service.AddAsync(product);

            result.IsSuccess.Should().BeTrue();
            result.Data.NameProduct.Should().Be("Test");
            result.Data.Code.Should().Be("123");
        }
        [Fact]
        public async Task AddAsync_Should_ReturnError_When_ProductIsValid()
        {
            var productTableMock = new Mock<IProductTableAccess>();
            productTableMock
                .Setup(x => x.AddAsync(It.IsAny<ProductEntity>()))
                .ThrowsAsync(new Exception("DB error"));
            var dbAccessMock = new Mock<IDataAccess>();
            dbAccessMock.Setup(x => x.ProductTable).Returns(productTableMock.Object);

            var dbServiceMock = new Mock<IDataBaseService>();
            dbServiceMock.Setup(x => x.DataBaseAccess).Returns(dbAccessMock.Object);

            var product = new ShopProjectWebServer.Models.Domain.Product.Product
            {
                NameProduct = "Test",
                Code = "123",
                Price = 100,
                Count = 5,
                Articule = "A1"
            };
            var service = new ProductService(dbServiceMock.Object);

            var result = await service.AddAsync(product);

            result.IsError.Should().BeTrue();
        }
        [Fact]
        public async Task AddAsync_Should_ReturnError_When_ProductIsExistsByBarCode()
        {
            var productTableMock = new Mock<IProductTableAccess>();
            productTableMock
               .Setup(x => x.AddAsync(It.IsAny<ProductEntity>()))
               .ReturnsAsync(new ProductEntity
               {
                   NameProduct = "Test",
                   Code = "123",
                   Price = 100,
                   Count = 5,
                   Articule = "A1"
               });
            productTableMock
                .Setup(x => x.ExistsByBarCode("123"))
                .ReturnsAsync(true);
            var dbAccessMock = new Mock<IDataAccess>();
            dbAccessMock.Setup(x => x.ProductTable).Returns(productTableMock.Object);

            var dbServiceMock = new Mock<IDataBaseService>();
            dbServiceMock.Setup(x => x.DataBaseAccess).Returns(dbAccessMock.Object);

            var product = new ShopProjectWebServer.Models.Domain.Product.Product
            {
                NameProduct = "Test",
                Code = "123",
                Price = 100,
                Count = 5,
                Articule = "A1"
            };
            var service = new ProductService(dbServiceMock.Object);

            var result = await service.AddAsync(product);

            result.IsError.Should().BeTrue();
            result.ErrorMessage.Should().Be("Товар існує");
        } 
    }
}
