using Calendar.ViewModels;

namespace Calendar.Views;

public partial class TasksPage : ContentPage
{
    public TasksPage(TasksViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}