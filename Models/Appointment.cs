using SQLite;

namespace Calendar.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public DateTime Date { get; set; }

        [NotNull]
        public TimeSpan StartTime { get; set; }

        [NotNull]
        public TimeSpan EndTime { get; set; }

        [NotNull]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Location { get; set; }

        [MaxLength(20)]
        public string Type { get; set; } = "Reunião"; // Reunião, Compromisso, etc.

        [MaxLength(10)]
        public string Color { get; set; } = "#6366f1";

        public bool IsRecurring { get; set; } = false;

        public bool HasReminder { get; set; } = false;

        public int ReminderMinutes { get; set; } = 15;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Propriedades calculadas
        public string TimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
        public string DateTimeDisplay => $"{Date:dd/MM/yyyy} às {StartTime:hh\\:mm}";
    }
}