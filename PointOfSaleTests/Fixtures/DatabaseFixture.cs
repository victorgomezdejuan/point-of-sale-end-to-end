using Npgsql;

namespace PointOfSaleTests.Fixtures;
public class DatabaseFixture : IDisposable {
    private static readonly string ServerConnectionString = "Host=localhost;Username=postgres;Password=victor";
    private static readonly string DatabaseConnectionString = ServerConnectionString + ";Database=postgress_database_catalog_tests";

    public DatabaseFixture() {
        ExecuteNonQuery(ServerConnectionString, "CREATE DATABASE postgress_database_catalog_tests");
        ExecuteNonQuery(DatabaseConnectionString, "CREATE TABLE Products (Code VARCHAR(255) PRIMARY KEY, Price DECIMAL(18,2))");
    }

    public void Dispose() => ExecuteNonQuery(ServerConnectionString, "DROP DATABASE postgress_database_catalog_tests WITH (FORCE)");

    public static string ConnectionString { get { return DatabaseConnectionString; } }

    private static void ExecuteNonQuery(string connectionString, string commandText) {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        using var command = new NpgsqlCommand(commandText, connection
        );
        command.ExecuteNonQuery();
        connection.Close();
    }
}