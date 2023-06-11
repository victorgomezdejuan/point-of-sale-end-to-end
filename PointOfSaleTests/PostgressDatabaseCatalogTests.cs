using PointOfSale;
using PointOfSale.ValueObjects;
using PointOfSaleTests.Fixtures;

namespace PointOfSaleTests;

[Collection("Database collection")]
public class PostgressDatabaseCatalogTests {
    [Fact]
    public void ProductFound() {
        // Arrange
        var productToBeFound = Product.FromCodeAndPrice("12345", Price.FromDecimal(77.55M));
        var catalog = new PostgressDatabaseCatalog(DatabaseFixture.ConnectionString);
        catalog.AddProduct(productToBeFound);

        // Act
        var foundProduct = catalog.FindProductByCode(productToBeFound.Code);

        // Assert
        Assert.Equal(productToBeFound, foundProduct);
    }

    [Fact]
    public void ProductNotFound() {
        // Arrange
        var catalog = new PostgressDatabaseCatalog(DatabaseFixture.ConnectionString);

        // Act
        var foundProduct = catalog.FindProductByCode("54321");

        // Assert
        Assert.Null(foundProduct);
    }
}
