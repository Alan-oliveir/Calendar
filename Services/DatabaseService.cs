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
        }

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
    }
}