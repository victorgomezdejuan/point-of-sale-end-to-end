using Moq;
using PointOfSale;
using PointOfSale.Interfaces;

namespace PointOfSaleTests;
public class BarcoreInterpreterCallsToBarcodeListenerTests {
    private readonly Mock<IBarcodeListener> barcodeListener;
    private readonly Mock<IViewRenderer> viewRenderer;

    public BarcoreInterpreterCallsToBarcodeListenerTests() {
        barcodeListener = new Mock<IBarcodeListener>();
        viewRenderer = new Mock<IViewRenderer>();
    }

    [Fact]
    public void ReadABarcode() {
        // Arrange
        TextReader textReader = new StringReader("12345\r\n");

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        barcodeListener.Verify(b => b.OnBarcode("12345"), Times.Once);
    }

    [Fact]
    public void ReadSeveralBarcodes() {
        // Arrange
        var textReader = new StringReader("12345\r\n11111\r\n22222\r\n");

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        barcodeListener.Verify(b => b.OnBarcode("12345"), Times.Once);
        barcodeListener.Verify(b => b.OnBarcode("11111"), Times.Once);
        barcodeListener.Verify(b => b.OnBarcode("22222"), Times.Once);
    }

    [Fact]
    public void ReadEmptyBarcode() {
        // Arrange
        var textReader = new StringReader("\r\n");

        // Act
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object, viewRenderer.Object);
        interpreter.Process();

        // Assert
        barcodeListener.Verify(b => b.OnBarcode(""), Times.Once);
    }
}
