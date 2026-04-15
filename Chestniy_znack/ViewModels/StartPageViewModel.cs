using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chestniy_znack.ViewModels;

public partial class StartPageViewModel : ViewModelBase
{
    [ObservableProperty] private string _documentBarcode = string.Empty;

    public string WelcomeTitle => "Сканирование упаковочного листа";

    public string Description =>
        "Отсканируйте штрихкод документа и нажмите \"Начать\". Если сканер завершает ввод клавишей Enter, переход выполнится автоматически.";

    public event EventHandler<string>? StartRequested;

    public bool CanStart => !string.IsNullOrWhiteSpace(DocumentBarcode);

    partial void OnDocumentBarcodeChanged(string value) => StartCommand.NotifyCanExecuteChanged();

    [RelayCommand(CanExecute = nameof(CanStart))]
    private void Start()
    {
        var barcode = DocumentBarcode.Trim();
        if (string.IsNullOrWhiteSpace(barcode))
        {
            return;
        }

        StartRequested?.Invoke(this, barcode);
    }

    public void Reset() => DocumentBarcode = string.Empty;
}
