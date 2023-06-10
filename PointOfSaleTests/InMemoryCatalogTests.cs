using PointOfSale;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;
public class CatalogTests {
    [Fact]
    public void ProductFound() {
        // Arrange
        var productToBeFound = Product.FromCodeAndPrice("12345", Price.FromDecimal(77.55M));

        var catalog = new InMemoryCatalog(new List<Product> { productToBeFound });

        // Act
        var foundProduct = catalog.FindProductByCode(productToBeFound.Code);

        // Assert
        Assert.Equal(productToBeFound, foundProduct);
    }

    [Fact]
    public void ProductNotFound() {
        // Arrange
        var catalog = new InMemoryCatalog(new List<Product>());

        // Act
        var foundProduct = catalog.FindProductByCode("54321");

        // Assert
        Assert.Null(foundProduct);
    }
}
