using PointOfSale;
using PointOfSale.Interfaces;
using PointOfSale.ValueObjects;
using System.IO.Pipes;

ICatalog catalog = new InMemoryCatalog(new List<Product> {
    Product.FromCodeAndPrice("12345", Price.FromDecimal(10.0m)),
    Product.FromCodeAndPrice("23456", Price.FromDecimal(20.0m)),
    Product.FromCodeAndPrice("34567", Price.FromDecimal(30.0m)),
    Product.FromCodeAndPrice("45678", Price.FromDecimal(40.0m)),
    Product.FromCodeAndPrice("56789", Price.FromDecimal(50.0m)),
});
using var stream = new NamedPipeClientStream(".", "DisplayPipe", PipeDirection.Out);
stream.Connect();
using var streamWriter = new StreamWriter(stream);
streamWriter.AutoFlush = true;
IBarcodeListener barcodeListener = new SaleHandler(catalog, new Display(streamWriter));
var barcodeInterpreter = new BarcoreInterpreter(Console.In, barcodeListener);
barcodeInterpreter.Process();