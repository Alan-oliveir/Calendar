using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calendar.Models;
using Calendar.Services;

namespace Calendar.ViewModels
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private DateTime _selectedDate;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _newAppointmentTitle;
        private string _newAppointmentDescription;
        private ObservableCollection<Appointment> _upcomingAppointments;
        private bool _isLoading;
        private Appointment _editingAppointment;

        public ScheduleViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _selectedDate = DateTime.Today;
            _startTime = new TimeSpan(9, 0, 0); // 09:00
            _endTime = new TimeSpan(10, 0, 0); // 10:00
            _upcomingAppointments = new ObservableCollection<Appointment>();

            SaveAppointmentCommand = new Command(async () => await SaveAppointment(), () => CanSaveAppointment());
            DeleteAppointmentCommand = new Command<Appointment>(async (appointment) => await DeleteAppointment(appointment));
            EditAppointmentCommand = new Command<Appointment>(async (appointment) => await EditAppointment(appointment));

            _ = LoadUpcomingAppointments();
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
                // Automaticamente ajustar o horário de fim se necessário
                if (value >= _endTime)
                {
                    EndTime = value.Add(TimeSpan.FromHours(1));
                }
                ((Command)SaveAppointmentCommand).ChangeCanExecute();
            }
        }

        public TimeSpan EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged();
                ((Command)SaveAppointmentCommand).ChangeCanExecute();
            }
        }

        public string NewAppointmentTitle
        {
            get => _newAppointmentTitle;
            set
            {
                _newAppointmentTitle = value;
                OnPropertyChanged();
                ((Command)SaveAppointmentCommand).ChangeCanExecute();
            }
        }

        public string NewAppointmentDescription
        {
            get => _newAppointmentDescription;
            set
            {
                _newAppointmentDescription = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Appointment> UpcomingAppointments
        {
            get => _upcomingAppointments;
            set
            {
                _upcomingAppointments = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasAppointments));
                OnPropertyChanged(nameof(HasNoAppointments));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool HasAppointments => UpcomingAppointments?.Count > 0;
        public bool HasNoAppointments => !HasAppointments;

        public ICommand SaveAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }
        public ICommand EditAppointmentCommand { get; }

        private bool CanSaveAppointment()
        {
            return !string.IsNullOrWhiteSpace(NewAppointmentTitle) &&
                   StartTime < EndTime;
        }

        private async Task SaveAppointment()
        {
            if (!CanSaveAppointment()) return;

            try
            {
                IsLoading = true;

                var appointment = _editingAppointment ?? new Appointment();
                appointment.Title = NewAppointmentTitle.Trim();
                appointment.Description = NewAppointmentDescription?.Trim() ?? string.Empty;
                appointment.Date = SelectedDate;
                appointment.StartTime = StartTime;
                appointment.EndTime = EndTime;

                if (_editingAppointment == null)
                {
                    appointment.CreatedAt = DateTime.Now;
                }

                appointment.UpdatedAt = DateTime.Now;

                await _databaseService.SaveAppointmentAsync(appointment);

                ClearForm();
                await LoadUpcomingAppointments();

                await Shell.Current.DisplayAlert("Sucesso",
                    _editingAppointment == null ? "Compromisso agendado com sucesso!" : "Compromisso atualizado com sucesso!",
                    "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao salvar compromisso: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DeleteAppointment(Appointment appointment)
        {
            if (appointment == null) return;

            try
            {
                bool confirmed = await Shell.Current.DisplayAlert("Confirmar",
                    $"Deseja excluir o compromisso '{appointment.Title}'?",
                    "Sim", "Não");

                if (confirmed)
                {
                    await _databaseService.DeleteAppointmentAsync(appointment);
                    await LoadUpcomingAppointments();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao excluir compromisso: " + ex.Message, "OK");
            }
        }

        private async Task EditAppointment(Appointment appointment)
        {
            if (appointment == null) return;

            _editingAppointment = appointment;
            NewAppointmentTitle = appointment.Title;
            NewAppointmentDescription = appointment.Description;
            SelectedDate = appointment.Date;
            StartTime = appointment.StartTime;
            EndTime = appointment.EndTime;
        }

        private void ClearForm()
        {
            NewAppointmentTitle = string.Empty;
            NewAppointmentDescription = string.Empty;
            SelectedDate = DateTime.Today;
            StartTime = new TimeSpan(9, 0, 0);
            EndTime = new TimeSpan(10, 0, 0);
            _editingAppointment = null;
        }

        private async Task LoadUpcomingAppointments()
        {
            try
            {
                IsLoading = true;
                var appointments = await _databaseService.GetAppointmentsAsync();
                UpcomingAppointments = new ObservableCollection<Appointment>(
                    appointments.Where(a => a.Date >= DateTime.Today)
                              .OrderBy(a => a.Date)
                              .ThenBy(a => a.StartTime));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao carregar compromissos: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
