using Calendar.ViewModels;

namespace Calendar.Views;

public partial class DiaryPage : ContentPage
{
    public DiaryPage(DiaryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}