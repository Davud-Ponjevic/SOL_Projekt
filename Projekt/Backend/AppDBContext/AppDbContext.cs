using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.AppDBContext
{
    public class AppDbContext : DbContext
    {
        // DbSet für Termine
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        // DbSet für Aufgaben
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hier kannst du bei Bedarf spezifische Konfigurationen für deine Modelle hinzufügen
            // Z.B.: modelBuilder.Entity<CalendarEvent>().Property(e => e.Name).HasMaxLength(100);
            // modelBuilder.Entity<TodoTask>().Property(t => t.Description).HasMaxLength(200);
        }
    }
}