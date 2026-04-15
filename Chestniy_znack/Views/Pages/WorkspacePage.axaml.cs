using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using Chestniy_znack.ViewModels;

namespace Chestniy_znack.Views.Pages;

public partial class WorkspacePage : UserControl
{
    public WorkspacePage()
    {
        InitializeComponent();
        PropertyChanged += (_, args) =>
        {
            if (args.Property == IsVisibleProperty)
            {
                OnVisibilityChanged(IsVisible);
            }
        };
    }

    private void ProductBarcodeTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter || DataContext is not WorkspacePageViewModel viewModel)
        {
            return;
        }

        viewModel.ConfirmProductBarcodeCommand.Execute(null);

        if (viewModel.CanScanKiz)
        {
            KizTextBox.Focus();
        }

        e.Handled = true;
    }

    private void KizTextBox_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter || DataContext is not WorkspacePageViewModel viewModel || !viewModel.CanScanKiz)
        {
            return;
        }

        viewModel.AddKizCommand.Execute(null);
        KizTextBox.Focus();
        e.Handled = true;
    }

    private void OnVisibilityChanged(bool isVisible)
    {
        if (!isVisible)
        {
            return;
        }

        Dispatcher.UIThread.Post(() =>
        {
            if (DataContext is not WorkspacePageViewModel viewModel)
            {
                return;
            }

            if (viewModel.CanScanKiz)
            {
                KizTextBox.Focus();
                return;
            }

            ProductBarcodeTextBox.Focus();
        });
    }
}
