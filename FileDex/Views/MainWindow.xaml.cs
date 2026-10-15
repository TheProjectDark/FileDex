using System.Windows;

namespace FileDex.Views;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    private void Exit_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    private void About_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "FileDex — metadata inspector\nVersion 0.1.0",
            "About FileDex",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}
