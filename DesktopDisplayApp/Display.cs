namespace DesktopDisplayApp;

public partial class Display : Form {
    private readonly StreamReader streamReader;

    public Display(StreamReader streamReader) {
        InitializeComponent();
        this.streamReader = streamReader;
    }

    private async void Timer_Tick(object sender, EventArgs e) {
        timer.Stop();
        string? temp;
        if ((temp = await streamReader.ReadLineAsync()) != null) {
            textBox.Text = temp;
        }
        timer.Start();
    }
}