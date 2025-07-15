using Calendar.Views;

namespace Calendar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar as rotas
            Routing.RegisterRoute("CalendarPage", typeof(CalendarPage));
            Routing.RegisterRoute("EventDetailPage", typeof(EventDetailPage));
        }
    }
}