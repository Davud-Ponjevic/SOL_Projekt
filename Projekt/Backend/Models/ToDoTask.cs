using System;

namespace Backend.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Note { get; set; } // Nullable machen
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
