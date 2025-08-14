using Avalonia.Controls;
using Movies.ViewModels;

namespace Movies.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MovieViewModel();
    }
}