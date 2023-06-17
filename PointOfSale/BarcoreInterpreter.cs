using PointOfSale.Interfaces;

namespace PointOfSale;
public class BarcoreInterpreter {
    private readonly TextReader textReader;
    private readonly IBarcodeListener barcodeListener;
    private readonly IViewRenderer viewRenderer;

    public BarcoreInterpreter(TextReader textReader, IBarcodeListener barcodeListener, IViewRenderer viewRenderer) {
        this.textReader = textReader;
        this.barcodeListener = barcodeListener;
        this.viewRenderer = viewRenderer;
    }

    public void Process() {
        while (true) {
            string? barcode = textReader.ReadLine();
            if (barcode is null)
                break;
            RenderCorrespondingResponse(barcode);
        }
    }

    private void RenderCorrespondingResponse(string barcode) {
        SaleOneItemView view = barcodeListener.OnBarcode(barcode);
        viewRenderer.Render(view);
    }
}
