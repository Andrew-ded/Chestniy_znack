using System.Collections.ObjectModel;

namespace Chestniy_znack.ViewModels;

public class BindingsPageViewModel : ViewModelBase
{
    public BindingsPageViewModel()
    {
        Mappings =
        [
            new ViewBindingItemViewModel("Views/MainWindow.axaml", "ViewModels/MainWindowViewModel.cs"),
            new ViewBindingItemViewModel("Views/Pages/StartPage.axaml", "ViewModels/StartPageViewModel.cs"),
            new ViewBindingItemViewModel("Views/Pages/WorkspacePage.axaml", "ViewModels/WorkspacePageViewModel.cs"),
            new ViewBindingItemViewModel("Views/Pages/BindingsPage.axaml", "ViewModels/BindingsPageViewModel.cs")
        ];
    }

    public ObservableCollection<ViewBindingItemViewModel> Mappings { get; }
}