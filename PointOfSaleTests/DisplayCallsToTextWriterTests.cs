using PointOfSale;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;
public class DisplayTests {
    private readonly TextWriter textWriter;
    private readonly Display display;

    public DisplayTests() {
        textWriter = new StringWriter();
        display = new Display(textWriter);
    }

    [Fact]
    public void DisplayPrice() {
        var price = Price.FromDecimal(77.55M);

        display.DisplayPrice(price);

        Assert.Equal("Price: $77.55", textWriter.ToString().Trim());
    }

    [Fact]
    public void DisplayProductNotFound() {
        display.DisplayProductNotFound("12345");

        Assert.Equal("Product not found: 12345", textWriter.ToString().Trim());
    }

    [Fact]
    public void DisplayEmptyCode() {
        display.DisplayEmptyCode();

        Assert.Equal("Empty barcode", textWriter.ToString().Trim());
    }

    internal void Dispose() {
        textWriter.Dispose();
    }
}
