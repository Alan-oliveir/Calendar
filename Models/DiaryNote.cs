using SQLite;

namespace Calendar.Models
{
    [Table("DiaryNotes")]
    public class DiaryNote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        [MaxLength(200)]
        public string Title { get; set; }

        [NotNull]
        [MaxLength(5000)]
        public string Content { get; set; }

        [NotNull]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}