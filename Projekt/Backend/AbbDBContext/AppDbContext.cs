using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.AbbDBContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<CalendarEvent> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Konfiguriere die Verbindung zur Datenbank
            optionsBuilder.UseSqlServer("Server=localhost;Database=SOLProjekt;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hier können weitere Konfigurationen für die Datenbankmodelle durchgeführt werden, falls erforderlich
            // Zum Beispiel: Primärschlüssel, Beziehungen, Indizes usw.
        }
    }
}