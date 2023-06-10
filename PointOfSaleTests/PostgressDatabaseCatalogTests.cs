using Npgsql;
using PointOfSale;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;
public class PostgressDatabaseCatalogTests {
    private static readonly string ServerConnectionString = "Host=localhost;Username=postgres;Password=victor";
    private static readonly string DatabaseConnectionString = ServerConnectionString + ";Database=postgress_database_catalog_tests";

    public PostgressDatabaseCatalogTests() {
        using var dropDatabaseConnection = new NpgsqlConnection(ServerConnectionString);
        dropDatabaseConnection.Open();
        using var dropDatabaseCommand = new NpgsqlCommand(
            "DROP DATABASE IF EXISTS postgress_database_catalog_tests WITH (FORCE)",
            dropDatabaseConnection
        );
        dropDatabaseCommand.ExecuteNonQuery();
        dropDatabaseConnection.Close();

        using var createDatabaseConnection = new NpgsqlConnection(ServerConnectionString);
        createDatabaseConnection.Open();
        using var createDatabaseCommand = new NpgsqlCommand(
            "CREATE DATABASE postgress_database_catalog_tests",
            createDatabaseConnection
        );
        createDatabaseCommand.ExecuteNonQuery();
        createDatabaseConnection.Close();

        using var createTableConnection = new NpgsqlConnection(DatabaseConnectionString);
        createTableConnection.Open();
        using var createTableCommand = new NpgsqlCommand(
            "CREATE TABLE Products (code VARCHAR(255) PRIMARY KEY, price DECIMAL(18,2))",
            createTableConnection
        );
        createTableCommand.ExecuteNonQuery();
        createTableConnection.Close();
    }

    [Fact]
    public void ProductFound() {
        // Arrange
        var productToBeFound = Product.FromCodeAndPrice("12345", Price.FromDecimal(77.55M));
        var catalog = new PostgressDatabaseCatalog(DatabaseConnectionString);
        catalog.AddProduct(productToBeFound);

        // Act
        var foundProduct = catalog.FindProductByCode(productToBeFound.Code);

        // Assert
        Assert.Equal(productToBeFound, foundProduct);
    }

    [Fact]
    public void ProductNotFound() {
        // Arrange
        var catalog = new PostgressDatabaseCatalog(DatabaseConnectionString);

        // Act
        var foundProduct = catalog.FindProductByCode("54321");

        // Assert
        Assert.Null(foundProduct);
    }
}
