using SQLite;

namespace Calendar.Models
{
    [Table("CalendarEvents")]
    public class CalendarEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public DateTime Date { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(20)]
        public string Category { get; set; }

        [MaxLength(10)]
        public string Color { get; set; } = "#6366f1"; // Cor padrão similar ao design

        public bool IsAllDay { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}