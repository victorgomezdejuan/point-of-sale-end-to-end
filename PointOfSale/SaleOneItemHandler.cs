using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class SaleOneItemHandler : IBarcodeListener {
    private readonly ICatalog catalog;

    public SaleOneItemHandler(ICatalog catalog) {
        this.catalog = catalog;
    }

    public SaleOneItemView OnBarcode(string barcode) {
        if (barcode == "") {
            return new SaleOneItemView("Empty barcode", new Dictionary<string, object>());
        }
        else {
            Product? product = catalog.FindProductByCode(barcode);
            if (product is null) {
                return new SaleOneItemView("Item not found", new Dictionary<string, object>() {
                    { "barcode", barcode }
                });
            }
            else {
                return new SaleOneItemView("Item found", new Dictionary<string, object>() {
                    { "price", product.Price }
                });
            }
        }
    }
}
