using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class TextWriterViewRenderer : IViewRenderer {
    private readonly TextWriter textWriter;

    public TextWriterViewRenderer(TextWriter textWriter) => this.textWriter = textWriter;

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

    private void DisplayPrice(Price price) => textWriter.WriteLine($"Price: {price}");

    private void DisplayProductNotFound(string code) => textWriter.WriteLine($"Product not found: {code}");

    private void DisplayEmptyCode() => textWriter.WriteLine("Empty barcode");
}
