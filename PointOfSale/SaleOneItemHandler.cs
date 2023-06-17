using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class SaleOneItemHandler : IBarcodeListener {
    private readonly ICatalog catalog;
    private readonly IDisplay display;

    public SaleOneItemHandler(ICatalog catalog, IDisplay display) {
        this.catalog = catalog;
        this.display = display;
    }

    public SaleOneItemView OnBarcode(string barcode) {
        if (barcode == "")
            display.DisplayEmptyCode();
        else {
            Product? product = catalog.FindProductByCode(barcode);
            if (product is null) {
                display.DisplayProductNotFound(barcode);
                return new SaleOneItemView("Item not found", new Dictionary<string, object>() {
                    { "barcode", barcode }
                });
            }
            else {
                display.DisplayPrice(product.Price);
                return new SaleOneItemView("Item found", new Dictionary<string, object>() {
                    { "price", product.Price }
                });
            }

        }

        return new SaleOneItemView("", new Dictionary<string, object>());
    }
}
