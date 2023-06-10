using PointOfSale.ValueObjects;

namespace PointOfSale.Interfaces;
public interface ICatalog {
    Product? FindProductByCode(string code);
}
