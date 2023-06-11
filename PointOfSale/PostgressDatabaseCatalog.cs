using Npgsql;
using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class PostgressDatabaseCatalog : ICatalog {
    private readonly string connectionString;

    public PostgressDatabaseCatalog(string connectionString) => this.connectionString = connectionString;

    public void AddProduct(Product productToBeFound) {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var insertProductCommand = new NpgsqlCommand(
            "INSERT INTO products (code, price) VALUES (@code, @price)",
            connection
        );
        insertProductCommand.Parameters.AddWithValue("code", productToBeFound.Code);
        insertProductCommand.Parameters.AddWithValue("price", productToBeFound.Price.Amount);
        insertProductCommand.ExecuteNonQuery();

        connection.Close();
    }

    public Product? FindProductByCode(string code) {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var selectProductCommand = new NpgsqlCommand(
            "SELECT code, price FROM products WHERE code = @code",
            connection
        );
        selectProductCommand.Parameters.AddWithValue("code", code);
        using var reader = selectProductCommand.ExecuteReader();

        if (!reader.Read()) {
            connection.Close();
            return null;
        }

        var product = Product.FromCodeAndPrice(reader.GetString(0), Price.FromDecimal(reader.GetDecimal(1)));

        connection.Close();

        return product;
    }
}
