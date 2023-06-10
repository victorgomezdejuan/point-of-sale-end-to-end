using PointOfSale.Interfaces;

namespace PointOfSale;
public class BarcoreInterpreter {
    private readonly TextReader textReader;
    private readonly IBarcodeListener barcodeListener;

    public BarcoreInterpreter(TextReader textReader, IBarcodeListener barcodeListener) {
        this.textReader = textReader;
        this.barcodeListener = barcodeListener;
    }

    public void Process() {
        while (true) {
            string? barcode = textReader.ReadLine();
            if (barcode is null)
                break;
            barcodeListener.OnBarcode(barcode);
        }
    }
}
