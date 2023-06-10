using System.Globalization;

namespace PointOfSale.ValueObjects;
public class Price {
    private Price(decimal amount) => Amount = amount;

    public decimal Amount { get; }

    public static Price FromDecimal(decimal amount) => new(amount);

    public override string ToString() => $"${Amount.ToString("0.00", CultureInfo.InvariantCulture)}";

    public override bool Equals(object? obj) => obj is Price price && Amount.Equals(price.Amount);

    public override int GetHashCode() => Amount.GetHashCode();
}
