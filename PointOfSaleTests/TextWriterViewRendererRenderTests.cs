using PointOfSale;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;
public class TextWriterViewRendererRenderTests {
    private readonly TextWriter textWriter;
    private readonly TextWriterViewRenderer display;

    public TextWriterViewRendererRenderTests() {
        textWriter = new StringWriter();
        display = new TextWriterViewRenderer(textWriter);
    }

    [Fact]
    public void ProductFound() {
        display.Render(
            new SaleOneItemView(
                "Item found",
                new Dictionary<string, object> { { "price", Price.FromDecimal(77.55M) } }
            )
        );

        Assert.Equal("Price: $77.55", textWriter.ToString().Trim());
    }

    [Fact]
    public void ProductNotFound() {
        display.Render(
            new SaleOneItemView(
                "Item not found",
                new Dictionary<string, object> { { "barcode", "12345" } }
            )
        );

        Assert.Equal("Product not found: 12345", textWriter.ToString().Trim());
    }

    [Fact]
    public void EmptyCode() {
        display.Render(
            new SaleOneItemView(
                "Empty barcode",
                new Dictionary<string, object>()
            )
        );

        Assert.Equal("Empty barcode", textWriter.ToString().Trim());
    }

    internal void Dispose() {
        textWriter.Dispose();
    }
}
