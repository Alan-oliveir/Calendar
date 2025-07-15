using Calendar.Services;
using Calendar.Views;

namespace Calendar
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            // Inicializar o banco de dados
            Database = new DatabaseService();

            // Definir a página inicial
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            // Configurar o tamanho da janela para desktop
            window.Width = 400;
            window.Height = 700;
            window.Title = "Agenda Pessoal";

            return window;
        }
    }
}