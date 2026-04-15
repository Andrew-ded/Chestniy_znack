using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Chestniy_znack.Models;
using Chestniy_znack.Services;

namespace Chestniy_znack.ViewModels;

public partial class WorkspacePageViewModel : ViewModelBase
{
    public WorkspacePageViewModel()
    {
        KizCodes.CollectionChanged += OnKizCodesCollectionChanged;
    }

    [ObservableProperty] private string _documentBarcode = string.Empty;
    [ObservableProperty] private string _productBarcode = string.Empty;
    [ObservableProperty] private string _currentKiz = string.Empty;
    [ObservableProperty] private string _statusMessage = string.Empty;
    [ObservableProperty] private bool _isProductBarcodeLocked;

    public ObservableCollection<string> KizCodes { get; } = [];

    public int KizCount => KizCodes.Count;

    public bool CanScanKiz => IsProductBarcodeLocked;

    public bool CanSave => !string.IsNullOrWhiteSpace(ProductBarcode) && KizCodes.Count > 0;

    public event EventHandler? SessionSaved;

    partial void OnProductBarcodeChanged(string value)
    {
        OnPropertyChanged(nameof(CanScanKiz));
        OnPropertyChanged(nameof(CanSave));
        SaveCommand.NotifyCanExecuteChanged();
    }

    partial void OnIsProductBarcodeLockedChanged(bool value)
    {
        OnPropertyChanged(nameof(CanScanKiz));
        OnPropertyChanged(nameof(CanSave));
        SaveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private void ConfirmProductBarcode()
    {
        if (IsProductBarcodeLocked)
        {
            return;
        }

        var barcode = ProductBarcode.Trim();
        if (string.IsNullOrWhiteSpace(barcode))
        {
            return;
        }

        ProductBarcode = barcode;
        IsProductBarcodeLocked = true;
        StatusMessage = "Второй ШК сохранен. Можно сканировать КИЗ.";
    }

    [RelayCommand]
    private void AddKiz()
    {
        if (!CanScanKiz)
        {
            StatusMessage = "Сначала отсканируйте второй ШК.";
            return;
        }

        var kiz = CurrentKiz.Trim();
        if (string.IsNullOrWhiteSpace(kiz))
        {
            return;
        }

        if (KizCodes.Contains(kiz))
        {
            StatusMessage = $"КИЗ {kiz} уже был отсканирован.";
            CurrentKiz = string.Empty;
            return;
        }

        KizCodes.Add(kiz);
        CurrentKiz = string.Empty;
        StatusMessage = $"Отсканировано КИЗ: {KizCodes.Count}";
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private void Save()
    {
        ProductBarcode = ProductBarcode.Trim();

        var record = new ScanSessionRecord
        {
            SavedAt = DateTime.Now,
            DocumentBarcode = DocumentBarcode,
            ProductBarcode = ProductBarcode,
            KizCodes = [.. KizCodes]
        };

        try
        {
            ScanSessionStorage.Save(record);
            StatusMessage = "Данные сохранены в scan-sessions.json.";
            SessionSaved?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка сохранения: {ex.Message}";
        }
    }

    public void BeginSession(string documentBarcode)
    {
        DocumentBarcode = documentBarcode;
        ProductBarcode = string.Empty;
        CurrentKiz = string.Empty;
        StatusMessage = string.Empty;
        IsProductBarcodeLocked = false;
        KizCodes.Clear();
        OnPropertyChanged(nameof(KizCount));
        OnPropertyChanged(nameof(CanScanKiz));
        OnPropertyChanged(nameof(CanSave));
        SaveCommand.NotifyCanExecuteChanged();
    }

    private void OnKizCodesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(KizCount));
        OnPropertyChanged(nameof(CanSave));
        SaveCommand.NotifyCanExecuteChanged();
    }
}
