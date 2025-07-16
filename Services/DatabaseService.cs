using SQLite;
using Calendar.Models;

namespace Calendar.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Calendar.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CalendarEvent>().Wait();
            _database.CreateTableAsync<DiaryNote>().Wait();
            _database.CreateTableAsync<Appointment>().Wait();
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        // Métodos para CalendarEvent
        public Task<List<CalendarEvent>> GetEventsAsync()
        {
            return _database.Table<CalendarEvent>().ToListAsync();
        }

        public Task<List<CalendarEvent>> GetEventsByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            return _database.Table<CalendarEvent>()
                .Where(e => e.Date >= startDate && e.Date < endDate)
                .ToListAsync();
        }

        public Task<List<CalendarEvent>> GetEventsByMonthAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1);
            return _database.Table<CalendarEvent>()
                .Where(e => e.Date >= startDate && e.Date < endDate)
                .ToListAsync();
        }

        public Task<CalendarEvent> GetEventAsync(int id)
        {
            return _database.Table<CalendarEvent>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveEventAsync(CalendarEvent calendarEvent)
        {
            calendarEvent.UpdatedAt = DateTime.Now;
            if (calendarEvent.Id != 0)
            {
                return _database.UpdateAsync(calendarEvent);
            }
            else
            {
                return _database.InsertAsync(calendarEvent);
            }
        }

        public Task<int> DeleteEventAsync(CalendarEvent calendarEvent)
        {
            return _database.DeleteAsync(calendarEvent);
        }

        public Task<bool> HasEventsOnDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            return _database.Table<CalendarEvent>()
                .Where(e => e.Date >= startDate && e.Date < endDate)
                .CountAsync()
                .ContinueWith(task => task.Result > 0);
        }

        public Task<List<CalendarEvent>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return _database.Table<CalendarEvent>()
                .Where(e => e.Date >= startDate.Date && e.Date <= endDate.Date)
                .ToListAsync();
        }

        // Métodos para DiaryNote
        public Task<List<DiaryNote>> GetDiaryNotesAsync()
        {
            return _database.Table<DiaryNote>()
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public Task<DiaryNote> GetDiaryNoteAsync(int id)
        {
            return _database.Table<DiaryNote>()
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveDiaryNoteAsync(DiaryNote note)
        {
            note.UpdatedAt = DateTime.Now;
            if (note.Id != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteDiaryNoteAsync(DiaryNote note)
        {
            return _database.DeleteAsync(note);
        }

        // Métodos para Appointment
        public Task<List<Appointment>> GetAppointmentsAsync()
        {
            return _database.Table<Appointment>()
                .OrderBy(a => a.Date)
                .ThenBy(a => a.StartTime)
                .ToListAsync();
        }

        public Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            return _database.Table<Appointment>()
                .Where(a => a.Date >= startDate && a.Date < endDate)
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }

        public Task<List<Appointment>> GetUpcomingAppointmentsAsync()
        {
            return _database.Table<Appointment>()
                .Where(a => a.Date >= DateTime.Today)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.StartTime)
                .ToListAsync();
        }

        public Task<Appointment> GetAppointmentAsync(int id)
        {
            return _database.Table<Appointment>()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveAppointmentAsync(Appointment appointment)
        {
            appointment.UpdatedAt = DateTime.Now;
            if (appointment.Id != 0)
            {
                return _database.UpdateAsync(appointment);
            }
            else
            {
                return _database.InsertAsync(appointment);
            }
        }

        public Task<int> DeleteAppointmentAsync(Appointment appointment)
        {
            return _database.DeleteAsync(appointment);
        }

        public Task<List<Appointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return _database.Table<Appointment>()
                .Where(a => a.Date >= startDate.Date && a.Date <= endDate.Date)
                .OrderBy(a => a.Date)
                .ThenBy(a => a.StartTime)
                .ToListAsync();
        }

        // Métodos para TaskItem:
        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>()
                .OrderBy(t => t.IsCompleted)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }

        public Task<List<TaskItem>> GetPendingTasksAsync()
        {
            return _database.Table<TaskItem>()
                .Where(t => !t.IsCompleted)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public Task<List<TaskItem>> GetCompletedTasksAsync()
        {
            return _database.Table<TaskItem>()
                .Where(t => t.IsCompleted)
                .OrderByDescending(t => t.CompletedAt)
                .ToListAsync();
        }

        public Task<List<TaskItem>> GetOverdueTasksAsync()
        {
            return _database.Table<TaskItem>()
                .Where(t => !t.IsCompleted && t.DueDate < DateTime.Today)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
        }

        public Task<TaskItem> GetTaskAsync(int id)
        {
            return _database.Table<TaskItem>()
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(TaskItem task)
        {
            task.UpdatedAt = DateTime.Now;
            if (task.Id != 0)
            {
                return _database.UpdateAsync(task);
            }
            else
            {
                return _database.InsertAsync(task);
            }
        }

        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }

        public Task<List<TaskItem>> GetTasksByPriorityAsync(string priority)
        {
            return _database.Table<TaskItem>()
                .Where(t => t.Priority == priority)
                .OrderBy(t => t.IsCompleted)
                .ThenBy(t => t.DueDate)
                .ToListAsync();
        }
    }
}