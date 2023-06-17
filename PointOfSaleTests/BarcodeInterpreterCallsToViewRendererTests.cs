using Moq;
using PointOfSale;
using PointOfSale.Interfaces;

namespace PointOfSaleTests;
public class BarcodeInterpreterCallsToViewRendererTests {
    private readonly Mock<IBarcodeListener> barcodeListener;
    private readonly Mock<IViewRenderer> viewRenderer;

    public BarcodeInterpreterCallsToViewRendererTests() {
        barcodeListener = new Mock<IBarcodeListener>();
        viewRenderer = new Mock<IViewRenderer>();
    }

    [Fact]
    public void ReadABarcode() {
        // Arrange
        TextReader textReader = new StringReader("12345\r\n");
        SaleOneItemView view = new("viewName", new Dictionary<string, object>() { { "key", "value" } });
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode("12345"))
            .Returns(view);

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        viewRenderer.Verify(v => v.Render(view), Times.Once);
    }

    [Fact]
    public void ReadSeveralBarcodes() {
        // Arrange
        var textReader = new StringReader("11111\r\n22222\r\n33333\r\n");
        SaleOneItemView view1 = new("viewName1", new Dictionary<string, object>() { { "key1", "value1" } });
        SaleOneItemView view2 = new("viewName2", new Dictionary<string, object>() { { "key2", "value2" } });
        SaleOneItemView view3 = new("viewName3", new Dictionary<string, object>() { { "key3", "value3" } });
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode("11111"))
            .Returns(view1);
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode("22222"))
            .Returns(view2);
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode("33333"))
            .Returns(view3);

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        viewRenderer.Verify(v => v.Render(view1), Times.Once);
        viewRenderer.Verify(v => v.Render(view2), Times.Once);
        viewRenderer.Verify(v => v.Render(view3), Times.Once);
    }

    [Fact]
    public void ReadEmptyBarcode() {
        // Arrange
        var textReader = new StringReader("\r\n");
        SaleOneItemView view = new("viewName", new Dictionary<string, object>() { { "key", "value" } });
        barcodeListener
            .Setup<SaleOneItemView>(b => b.OnBarcode(""))
            .Returns(view);

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        viewRenderer.Verify(v => v.Render(view), Times.Once);
    }
}
