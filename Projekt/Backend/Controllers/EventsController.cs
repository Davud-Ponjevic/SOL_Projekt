using Backend.AppDBContext;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public IEnumerable<CalendarEvent> GetEvents()
        {
            return _context.CalendarEvents.ToList();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public ActionResult<CalendarEvent> GetEvent(int id)
        {
            var calendarEvent = _context.CalendarEvents.Find(id);

            if (calendarEvent == null)
            {
                return NotFound();
            }

            return calendarEvent;
        }

        // POST: api/Events
        [HttpPost]
        public ActionResult<CalendarEvent> PostEvent(CalendarEvent calendarEvent)
        {
            _context.CalendarEvents.Add(calendarEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { id = calendarEvent.Id }, calendarEvent);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendarEvent).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var calendarEvent = _context.CalendarEvents.Find(id);
            if (calendarEvent == null)
            {
                return NotFound();
            }

            // Löschen Sie zuerst die verknüpften Aufgaben
            var tasksToDelete = _context.ToDoTasks.Where(t => t.Id == id);
            _context.ToDoTasks.RemoveRange(tasksToDelete);

            // Dann löschen Sie das Ereignis
            _context.CalendarEvents.Remove(calendarEvent);

            // Speichern Sie die Änderungen
            _context.SaveChanges();

            return NoContent();
        }
    }
}
