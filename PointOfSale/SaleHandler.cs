using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class SaleHandler : IBarcodeListener {
    private readonly ICatalog catalog;
    private readonly IDisplay display;

    public SaleHandler(ICatalog catalog, IDisplay display) {
        this.catalog = catalog;
        this.display = display;
    }

    public void OnBarcode(string barcode) {
        if (barcode == "")
            display.DisplayEmptyCode();
        else {
            Product? product = catalog.FindProductByCode(barcode);
            DisplayProductInfo(barcode, product);
        }
    }

    private void DisplayProductInfo(string barcode, Product? product) {
        if (product is null)
            display.DisplayProductNotFound(barcode);
        else
            display.DisplayPrice(product.Price);
    }
}
