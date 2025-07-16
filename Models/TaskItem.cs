using SQLite;

namespace Calendar.Models
{
    [Table("Tasks")]
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [NotNull]
        public DateTime DueDate { get; set; }

        [NotNull]
        [MaxLength(20)]
        public string Priority { get; set; } = "Baixa"; // Baixa, Média, Alta

        [MaxLength(50)]
        public string Category { get; set; } = "Geral";

        [MaxLength(10)]
        public string Color { get; set; } = "#6366f1";

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Propriedades calculadas
        public string PriorityIcon => Priority switch
        {
            "Alta" => "🔴",
            "Média" => "🟡",
            "Baixa" => "🟢",
            _ => "🟢"
        };

        public string StatusIcon => IsCompleted ? "✅" : "⏳";

        public string DueDateDisplay => DueDate.ToString("dd/MM/yyyy");

        public bool IsOverdue => !IsCompleted && DueDate.Date < DateTime.Today;

        public string StatusText => IsCompleted ? "Concluída" : (IsOverdue ? "Atrasada" : "Pendente");
    }
}