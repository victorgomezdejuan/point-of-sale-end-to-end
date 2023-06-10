using System.IO.Pipes;

namespace DesktopDisplayApp;

internal static class Program {
    [STAThread]
    static void Main() {
        using var stream = new NamedPipeServerStream("DisplayPipe", PipeDirection.In);
        stream.WaitForConnection();
        using var streamReader = new StreamReader(stream);
        ApplicationConfiguration.Initialize();
        var mainForm = new Display(streamReader);
        Application.Run(mainForm);
    }
}