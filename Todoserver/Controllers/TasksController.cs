using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Todoserver.Models;


namespace Todoserver.Controllers
{   
    [Authorize]
    public class TasksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tasks
        public IQueryable<Tasks> GetTasks()
        {
            string owner = HttpContext.Current.User.Identity.Name;

            return db.Tasks.Where(_tasks => _tasks.OwnerId== owner);

        }

        // GET: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult GetTasks(int id)
        {
            string owner = HttpContext.Current.User.Identity.Name;
            Tasks tasks = db.Tasks.FirstOrDefault(_tasks => _tasks.Id == id && _tasks.OwnerId == owner);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        // PUT: api/Tasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTasks(int id, Tasks tasks)
        {
            string owner = HttpContext.Current.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (db.Tasks.AsNoTracking().FirstOrDefault(_tasks => _tasks.Id == id && _tasks.OwnerId == owner)== null)
            {
                return BadRequest();
            }
            if (id != tasks.Id)
            {
                return BadRequest();
            }

            tasks.OwnerId = owner;
            db.Entry(tasks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tasks
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult PostTasks(Tasks tasks)
        {
            string owner = HttpContext.Current.User.Identity.Name;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tasks.OwnerId = owner;

            db.Tasks.Add(tasks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tasks.Id }, tasks);
        }

        // DELETE: api/Tasks/5
        [ResponseType(typeof(Tasks))]
        public IHttpActionResult DeleteTasks(int id)
        {
            string owner = HttpContext.Current.User.Identity.Name;
            Tasks tasks = db.Tasks.FirstOrDefault(_tasks => _tasks.Id == id && _tasks.OwnerId == owner);
            if (tasks == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(tasks);
            db.SaveChanges();

            return Ok(tasks);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TasksExists(int id)
        {
            return db.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}