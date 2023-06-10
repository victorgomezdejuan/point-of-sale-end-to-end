namespace PointOfSale.ValueObjects;

public class Product {
    private Product(string code, Price price) {
        Code = code;
        Price = price;
    }

    public string Code { get; }
    public Price Price { get; }

    public static Product FromCodeAndPrice(string code, Price price) => new(code, price);

    public override bool Equals(object? obj) => obj is Product product && Code.Equals(product.Code) && Price.Equals(product.Price);

    public override int GetHashCode() => Code.GetHashCode() ^ Price.GetHashCode();
}