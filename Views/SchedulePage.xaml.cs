using Calendar.ViewModels;

namespace Calendar.Views;

public partial class SchedulePage : ContentPage
{
    public SchedulePage(ScheduleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}