using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class Display : IViewRenderer {
    private readonly TextWriter textWriter;

    public Display(TextWriter textWriter) => this.textWriter = textWriter;

    public void DisplayPrice(Price price) => textWriter.WriteLine($"Price: {price}");

    public void DisplayProductNotFound(string code) => textWriter.WriteLine($"Product not found: {code}");

    public void DisplayEmptyCode() => textWriter.WriteLine("Empty barcode");

    public void Render(SaleOneItemView saleOneItemView) {
        if (saleOneItemView.Name.Equals("Item found")) {
            DisplayPrice((Price)saleOneItemView.Model["price"]);
        }
        else if (saleOneItemView.Name.Equals("Item not found")) {
            DisplayProductNotFound((string)saleOneItemView.Model["barcode"]);
        }
        else if (saleOneItemView.Name.Equals("Empty barcode")) {
            DisplayEmptyCode();
        }
    }
}
