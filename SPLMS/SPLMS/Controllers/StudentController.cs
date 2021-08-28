using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AuthenticationAPI.Entities;
using AuthenticationAPI.Infrastructure;
using AuthenticationAPI.Models;


namespace AuthenticationAPI.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: api/Student
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Student/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Student
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Student/5
        [Route("{id:guid}")]
        [HttpPut]
        public IHttpActionResult putStudent([FromUri] string id, StudentBindingModel std)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var student = new Entities.Student()
            {
                Id = id,
                RollNo = std.Roll,
                Batch = std.Batch,
                GroupId = std.GroupId
            };

            db.Students.Attach(student);
            db.Entry(student).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        private bool StudentExists(string userId)
        {
            return db.Students.Count(e => e.Id == userId) > 0;
        }


        // DELETE: api/Student/5
        public void Delete(int id)
        {
        }
    }
}
