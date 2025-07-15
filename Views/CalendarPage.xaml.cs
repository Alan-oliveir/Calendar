using Calendar.ViewModels;

namespace Calendar.Views
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage(CalendarViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}