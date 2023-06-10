using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;

namespace PointOfSale;
public class Display : IDisplay {
    private readonly TextWriter textWriter;

    public Display(TextWriter textWriter) => this.textWriter = textWriter;

    public void DisplayPrice(Price price) => textWriter.WriteLine($"Price: {price}");

    public void DisplayProductNotFound(string code) => textWriter.WriteLine($"Product not found: {code}");

    public void DisplayEmptyCode() => textWriter.WriteLine("Empty barcode");
}
