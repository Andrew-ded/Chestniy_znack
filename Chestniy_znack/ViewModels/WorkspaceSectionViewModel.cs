namespace Chestniy_znack.ViewModels;

public class WorkspaceSectionViewModel : ViewModelBase
{
    public WorkspaceSectionViewModel(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public string Title { get; }

    public string Description { get; }
}