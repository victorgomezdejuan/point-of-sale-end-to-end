using Moq;
using PointOfSale;
using PointOfSale.Interfaces;

namespace PointOfSaleTests;
public class BarcoreInterpreterTests {
    [Fact]
    public void ReadABarcode() {
        TextReader textReader = new StringReader("12345\r\n");
        var barcodeListener = new Mock<IBarcodeListener>();
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object);

        interpreter.Process();

        barcodeListener.Verify(b => b.OnBarcode("12345"), Times.Once);
    }

    [Fact]
    public void ReadSeveralBarcodes() {
        var textReader = new StringReader("12345\r\n11111\r\n22222\r\n");
        var barcodeListener = new Mock<IBarcodeListener>();
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object);

        interpreter.Process();

        barcodeListener.Verify(b => b.OnBarcode("12345"), Times.Once);
        barcodeListener.Verify(b => b.OnBarcode("11111"), Times.Once);
        barcodeListener.Verify(b => b.OnBarcode("22222"), Times.Once);
    }

    [Fact]
    public void ReadEmptyBarcode() {
        var textReader = new StringReader("\r\n");
        var barcodeListener = new Mock<IBarcodeListener>();
        BarcoreInterpreter interpreter = new(textReader, barcodeListener.Object);

        interpreter.Process();

        barcodeListener.Verify(b => b.OnBarcode(""), Times.Once);
    }
}
