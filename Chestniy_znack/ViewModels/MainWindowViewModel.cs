using System;
using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Chestniy_znack.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private bool _isSidebarVisible = true;
    [ObservableProperty] private bool _isStartPage = true;
    [ObservableProperty] private bool _isWorkspacePage;
    [ObservableProperty] private bool _isBindingsPage;
    [ObservableProperty] private bool _isLightTheme;

    public MainWindowViewModel()
    {
        UserName = Environment.UserName;
        StartPageViewModel = new StartPageViewModel();
        WorkspacePageViewModel = new WorkspacePageViewModel();
        BindingsPageViewModel = new BindingsPageViewModel();

        StartPageViewModel.StartRequested += (_, barcode) =>
        {
            WorkspacePageViewModel.BeginSession(barcode);
            GoToWorkspacePage();
        };

        WorkspacePageViewModel.SessionSaved += (_, _) =>
        {
            StartPageViewModel.Reset();
            GoToStartPage();
        };
    }

    public string UserName { get; }

    public double SidebarWidth => IsSidebarVisible ? 220 : 0;

    public bool ShowBackButton => !IsStartPage;

    public string SectionTitle => IsStartPage
        ? "Старт"
        : IsWorkspacePage
            ? "Заполнение"
            : "Служебная страница";

    public string ThemeButtonText => IsLightTheme ? "Темная тема" : "Светлая тема";

    public StartPageViewModel StartPageViewModel { get; }

    public WorkspacePageViewModel WorkspacePageViewModel { get; }

    public BindingsPageViewModel BindingsPageViewModel { get; }

    partial void OnIsSidebarVisibleChanged(bool value) => OnPropertyChanged(nameof(SidebarWidth));

    partial void OnIsStartPageChanged(bool value)
    {
        OnPropertyChanged(nameof(ShowBackButton));
        OnPropertyChanged(nameof(SectionTitle));
    }

    partial void OnIsWorkspacePageChanged(bool value)
    {
        OnPropertyChanged(nameof(ShowBackButton));
        OnPropertyChanged(nameof(SectionTitle));
    }

    partial void OnIsBindingsPageChanged(bool value)
    {
        OnPropertyChanged(nameof(ShowBackButton));
        OnPropertyChanged(nameof(SectionTitle));
    }

    partial void OnIsLightThemeChanged(bool value)
    {
        if (Application.Current is not null)
        {
            Application.Current.RequestedThemeVariant = value
                ? ThemeVariant.Light
                : ThemeVariant.Dark;
        }

        OnPropertyChanged(nameof(ThemeButtonText));
    }

    [RelayCommand]
    private void GoToStartPage()
    {
        IsStartPage = true;
        IsWorkspacePage = false;
        IsBindingsPage = false;
    }

    [RelayCommand]
    private void GoToWorkspacePage()
    {
        IsStartPage = false;
        IsWorkspacePage = true;
        IsBindingsPage = false;
    }

    [RelayCommand]
    private void ToggleSidebar()
    {
        IsSidebarVisible = !IsSidebarVisible;
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        IsLightTheme = !IsLightTheme;
    }
}
