using Calendar.Models;
using Calendar.Services;

namespace Calendar.Views
{
    [QueryProperty(nameof(EventId), "eventId")]
    [QueryProperty(nameof(SelectedDate), "selectedDate")]
    public partial class EventDetailPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private CalendarEvent _currentEvent;
        private string _selectedColor = "#6366f1";

        public string EventId { get; set; }
        public string SelectedDate { get; set; }

        public EventDetailPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            CategoryPicker.SelectedIndex = 0; // Selecionar primeira categoria por padrão
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadEventData();
        }

        private async Task LoadEventData()
        {
            if (!string.IsNullOrEmpty(EventId) && int.TryParse(EventId, out int eventId))
            {
                // Modo edição
                _currentEvent = await _databaseService.GetEventAsync(eventId);
                if (_currentEvent != null)
                {
                    TitleEntry.Text = _currentEvent.Title;
                    EventDatePicker.Date = _currentEvent.Date;
                    DescriptionEditor.Text = _currentEvent.Description;
                    
                    // Definir categoria
                    var categoryIndex = CategoryPicker.Items.IndexOf(_currentEvent.Category);
                    if (categoryIndex >= 0)
                        CategoryPicker.SelectedIndex = categoryIndex;
                    
                    _selectedColor = _currentEvent.Color;
                    DeleteButton.IsVisible = true;
                    Title = "Editar Evento";
                }
            }
            else if (!string.IsNullOrEmpty(SelectedDate) && DateTime.TryParse(SelectedDate, out DateTime selectedDate))
            {
                // Modo criação
                _currentEvent = new CalendarEvent
                {
                    Date = selectedDate,
                    Color = _selectedColor
                };
                EventDatePicker.Date = selectedDate;
                Title = "Novo Evento";
            }
            else
            {
                // Modo criação com data atual
                _currentEvent = new CalendarEvent
                {
                    Date = DateTime.Today,
                    Color = _selectedColor
                };
                EventDatePicker.Date = DateTime.Today;
                Title = "Novo Evento";
            }
        }

        private void OnColorSelected(object sender, EventArgs e)
        {
            if (sender is Frame colorFrame)
            {
                _selectedColor = colorFrame.BackgroundColor.ToHex();
                
                // Resetar todas as bordas
                var parent = colorFrame.Parent as StackLayout;
                foreach (var child in parent.Children)
                {
                    if (child is Frame frame)
                    {
                        frame.BorderColor = Colors.Transparent;
                    }
                }
                
                // Destacar cor selecionada
                colorFrame.BorderColor = Colors.Black;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Erro", "Por favor, insira um título para o evento.", "OK");
                return;
            }

            _currentEvent.Title = TitleEntry.Text.Trim();
            _currentEvent.Date = EventDatePicker.Date;
            _currentEvent.Description = DescriptionEditor.Text?.Trim() ?? "";
            _currentEvent.Category = CategoryPicker.SelectedItem?.ToString() ?? "Outros";
            _currentEvent.Color = _selectedColor;

            try
            {
                await _databaseService.SaveEventAsync(_currentEvent);
                await DisplayAlert("Sucesso", "Evento salvo com sucesso!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao salvar evento: {ex.Message}", "OK");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Cancelar", "Deseja realmente cancelar? As alterações não serão salvas.", "Sim", "Não");
            if (confirm)
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (_currentEvent == null || _currentEvent.Id == 0)
                return;

            bool confirm = await DisplayAlert("Deletar Evento", "Tem certeza que deseja deletar este evento?", "Deletar", "Cancelar");
            if (confirm)
            {
                try
                {
                    await _databaseService.DeleteEventAsync(_currentEvent);
                    await DisplayAlert("Sucesso", "Evento deletado com sucesso!", "OK");
                    await Shell.Current.GoToAsync("..");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Erro ao deletar evento: {ex.Message}", "OK");
                }
            }
        }
    }
}