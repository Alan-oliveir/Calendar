using Calendar.Views;

namespace Calendar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            // Navegar para a página do calendário via Shell
            await Shell.Current.GoToAsync("//CalendarPage");
        }
    }
}