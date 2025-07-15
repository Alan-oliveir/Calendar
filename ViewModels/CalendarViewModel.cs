using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calendar.Models;
using Calendar.Services;

namespace Calendar.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private DateTime _currentDate;
        private DateTime _selectedDate;
        private ObservableCollection<CalendarDay> _calendarDays;
        private ObservableCollection<CalendarEvent> _selectedDateEvents;
        private bool _isLoading;

        public CalendarViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _currentDate = DateTime.Now;
            _selectedDate = DateTime.Now;
            _calendarDays = new ObservableCollection<CalendarDay>();
            _selectedDateEvents = new ObservableCollection<CalendarEvent>();

            PreviousMonthCommand = new Command(async () => await PreviousMonth());
            NextMonthCommand = new Command(async () => await NextMonth());
            DaySelectedCommand = new Command<CalendarDay>(async (day) => await OnDaySelected(day));
            AddEventCommand = new Command(async () => await AddEvent());
            DeleteEventCommand = new Command<CalendarEvent>(async (evt) => await DeleteEvent(evt));

            _ = LoadCalendar();
        }

        public DateTime CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MonthYearText));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                _ = LoadEventsForSelectedDate();
            }
        }

        public ObservableCollection<CalendarDay> CalendarDays
        {
            get => _calendarDays;
            set
            {
                _calendarDays = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<CalendarEvent> SelectedDateEvents
        {
            get => _selectedDateEvents;
            set
            {
                _selectedDateEvents = value;
                OnPropertyChanged();
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

        public string MonthYearText => _currentDate.ToString("MMMM yyyy");

        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand DaySelectedCommand { get; }
        public ICommand AddEventCommand { get; }
        public ICommand DeleteEventCommand { get; }

        private async Task LoadCalendar()
        {
            try
            {
                IsLoading = true;

                var firstDayOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var startDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);
                var endDate = lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek);

                var events = await _databaseService.GetEventsByMonthAsync(_currentDate.Year, _currentDate.Month);

                var calendarDays = new ObservableCollection<CalendarDay>();

                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    var dayEvents = events.Where(e => e.Date.Date == date.Date).ToList();

                    calendarDays.Add(new CalendarDay
                    {
                        Date = date,
                        Day = date.Day,
                        IsCurrentMonth = date.Month == _currentDate.Month,
                        IsToday = date.Date == DateTime.Today,
                        IsSelected = date.Date == _selectedDate.Date,
                        HasEvents = dayEvents.Any(),
                        EventsCount = dayEvents.Count
                    });
                }

                CalendarDays = calendarDays;
            }
            catch (Exception ex)
            {
                // Log do erro
                await Shell.Current.DisplayAlert("Erro", "Erro ao carregar calendário", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadEventsForSelectedDate()
        {
            try
            {
                var events = await _databaseService.GetEventsByDateAsync(_selectedDate);
                SelectedDateEvents = new ObservableCollection<CalendarEvent>(events);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao carregar eventos", "OK");
            }
        }

        private async Task PreviousMonth()
        {
            CurrentDate = _currentDate.AddMonths(-1);
            await LoadCalendar();
        }

        private async Task NextMonth()
        {
            CurrentDate = _currentDate.AddMonths(1);
            await LoadCalendar();
        }

        private async Task OnDaySelected(CalendarDay day)
        {
            if (day == null) return;

            // Atualizar seleção visual
            foreach (var calendarDay in CalendarDays)
            {
                calendarDay.IsSelected = calendarDay.Date.Date == day.Date.Date;
            }

            SelectedDate = day.Date;
        }

        private async Task AddEvent()
        {
            await Shell.Current.GoToAsync($"EventDetailPage?selectedDate={_selectedDate:yyyy-MM-dd}");
        }

        private async Task DeleteEvent(CalendarEvent eventToDelete)
        {
            if (eventToDelete == null) return;

            try
            {
                await _databaseService.DeleteEventAsync(eventToDelete);
                await LoadCalendar();
                await LoadEventsForSelectedDate();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", "Erro ao deletar evento", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CalendarDay : INotifyPropertyChanged
    {
        private bool _isSelected;

        public DateTime Date { get; set; }
        public int Day { get; set; }
        public bool IsCurrentMonth { get; set; }
        public bool IsToday { get; set; }
        public bool HasEvents { get; set; }
        public int EventsCount { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
