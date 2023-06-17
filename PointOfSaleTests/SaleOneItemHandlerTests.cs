using FluentAssertions;
using Moq;
using PointOfSale;
using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;

public class SaleOneItemHandlerTests {
    [Fact]
    public void ItemFound() {
        // Arrange
        var productToBeFound = Product.FromCodeAndPrice("12345", Price.FromDecimal(77.55M));

        var catalogMock = new Mock<ICatalog>();
        catalogMock.Setup(c => c.FindProductByCode(productToBeFound.Code)).Returns(productToBeFound);

        var saleHandler = new SaleOneItemHandler(catalogMock.Object);

        // Act
        SaleOneItemView view = saleHandler.OnBarcode(productToBeFound.Code);

        // Assert
        catalogMock.Verify(d => d.FindProductByCode(productToBeFound.Code));
        view.Name.Should().Be("Item found");
        view.Model.Should().BeEquivalentTo(
            new Dictionary<string, object>(
                new List<KeyValuePair<string, object>>() {
                    new("price", Price.FromDecimal(77.55M))
                }
            )
        );
    }

    [Fact]
    public void ItemNotFound() {
        // Arrange
        var productToBeFound = Product.FromCodeAndPrice("11111", Price.FromDecimal(11.22M));

        var catalogMock = new Mock<ICatalog>();
        catalogMock.Setup(c => c.FindProductByCode(productToBeFound.Code)).Returns((Product)null);

        var saleHandler = new SaleOneItemHandler(catalogMock.Object);

        // Act
        SaleOneItemView view = saleHandler.OnBarcode(productToBeFound.Code);

        // Assert
        catalogMock.Verify(d => d.FindProductByCode(productToBeFound.Code));
        view.Name.Should().Be("Item not found");
        view.Model.Should().BeEquivalentTo(
            new Dictionary<string, object>(
                new List<KeyValuePair<string, object>>() {
                    new("barcode", "11111")
                }
            )
        );
    }

    [Fact]
    public void EmptyBarcode() {
        // Arrange
        var catalogMock = new Mock<ICatalog>();

        var saleHandler = new SaleOneItemHandler(catalogMock.Object);

        // Act
        SaleOneItemView view = saleHandler.OnBarcode("");

        // Assert
        catalogMock.Verify(d => d.FindProductByCode(""), Times.Never);
        view.Name.Should().Be("Empty barcode");
        view.Model.Should().BeEquivalentTo(
            new Dictionary<string, object>()
        );
    }
}