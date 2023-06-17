namespace PointOfSale.Interfaces;
public interface IBarcodeListener {
    SaleOneItemView OnBarcode(string barcode);
}
