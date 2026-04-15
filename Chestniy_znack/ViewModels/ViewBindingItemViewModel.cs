namespace Chestniy_znack.ViewModels;

public class ViewBindingItemViewModel : ViewModelBase
{
    public ViewBindingItemViewModel(string viewPath, string viewModelPath)
    {
        ViewPath = viewPath;
        ViewModelPath = viewModelPath;
    }

    public string ViewPath { get; }

    public string ViewModelPath { get; }
}