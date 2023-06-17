﻿using PointOfSale;
using PointOfSale.ValueObjects;

namespace PointOfSaleTests;
public class DisplayRenderTests {
    private readonly TextWriter textWriter;
    private readonly Display display;

    public DisplayRenderTests() {
        textWriter = new StringWriter();
        display = new Display(textWriter);
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

    internal void Dispose() {
        textWriter.Dispose();
    }
}