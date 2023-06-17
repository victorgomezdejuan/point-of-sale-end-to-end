using Moq;
using PointOfSale;
using PointOfSale.Interfaces;

namespace PointOfSaleTests;
public class BarcodeInterpreterCallsToViewRendererTests {
    [Fact]
    public void ReadABarcode() {
        TextReader textReader = new StringReader("12345\r\n");
        var barcodeListener = new Mock<IBarcodeListener>();
        var viewRenderer = new Mock<IViewRenderer>();
        SaleOneItemView view = new("viewName", new Dictionary<string, object>() { { "key", "value" } });
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode("12345"))
            .Returns(view);
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);

        interpreter.Process();

        viewRenderer.Verify(v => v.Render(view), Times.Once);
    }
}
