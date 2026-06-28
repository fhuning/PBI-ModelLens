using System.Windows;
using PowerBICleanup.UI.ViewModels;

namespace PowerBICleanup.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}
