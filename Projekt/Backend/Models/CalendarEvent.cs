using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Note { get; set; } // Nullable machen
        public DateTime EventDate { get; set; }
        public required List<ToDoTask> Tasks { get; set; } // Nullable machen oder initialisieren
    }
}
