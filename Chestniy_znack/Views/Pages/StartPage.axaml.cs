using Avalonia.Controls;
using Avalonia.Input;
using Chestniy_znack.ViewModels;

namespace Chestniy_znack.Views.Pages;

public partial class StartPage : UserControl
{
    public StartPage()
    {
        InitializeComponent();
        AttachedToVisualTree += (_, _) => DocumentBarcodeTextBox.Focus();
    }

    private void DocumentBarcodeTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter || DataContext is not StartPageViewModel viewModel)
        {
            return;
        }

        if (viewModel.StartCommand.CanExecute(null))
        {
            viewModel.StartCommand.Execute(null);
            e.Handled = true;
        }
    }
}
