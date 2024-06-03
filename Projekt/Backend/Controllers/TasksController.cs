using Backend.AppDBContext;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<ToDoTask> GetTasks()
        {
            return _context.ToDoTasks.ToList();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public ActionResult<ToDoTask> GetTask(int id)
        {
            var task = _context.ToDoTasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // POST: api/Tasks
        [HttpPost]
        public ActionResult<ToDoTask> PostTask(ToDoTask task)
        {
            _context.ToDoTasks.Add(task);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public IActionResult PutTask(int id, ToDoTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.ToDoTasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.ToDoTasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
