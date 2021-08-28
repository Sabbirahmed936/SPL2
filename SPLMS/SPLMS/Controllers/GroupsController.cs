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
    //[Authorize]
    [RoutePrefix("api/groups")]
    public class GroupsController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Groups
        [Route("all")]
        public IQueryable<Group> GetGroups()
        {
            return db.Groups;
        }

        // GET: api/Groups/5
        [Route("group/{id}", Name = "GetGroupById")]
        public async Task<IHttpActionResult> GetGroupById([FromUri]string id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(group));
        }

        //[Authorize]
        [Route("groupname/{groupname}")]
        public IHttpActionResult GetGroupByName([FromUri]string groupname)
        {
            var group = db.Groups.FirstOrDefault(g => g.GroupName == groupname);

            if (group != null)
            {
                return Ok(this.TheModelFactory.Create(group));
            }

            return NotFound();

        }

        // PUT: api/Groups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroup(string id, Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != group.GroupId)
            {
                return BadRequest();
            }

            db.Entry(group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/groups/create
        [Route("create")]
        [ResponseType(typeof(Group))]
        [HttpPost]
        public async Task<IHttpActionResult> PostGroup(GroupBindingModel groupBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            var group = new Group()
            {
                GroupId = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString(),
                GroupName = groupBindingModel.GroupName,
                ProjectTopic = groupBindingModel.ProjectTopic,
                Year = groupBindingModel.Year,
                TeacherId = groupBindingModel.TeacherId,
                CourseCode = groupBindingModel.CourseCode
            };

            db.Groups.Add(group);

            //var student1 = db.Students.FirstOrDefault(s => s.Id == groupBindingModel.StudentId1);
            //student1.GroupId = group.GroupId;

            //var student2 = db.Students.FirstOrDefault(s => s.Id == groupBindingModel.StudentId2);
            //student2.GroupId = group.GroupId;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GroupExists(group.GroupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var locationHeader = new Uri(Url.Link("GetGroupById", new { id = group.GroupId }));
            return Created(locationHeader, TheModelFactory.Create(group));
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Group))]
        public IHttpActionResult DeleteGroup(string id)
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return NotFound();
            }

            db.Groups.Remove(group);
            db.SaveChanges();

            return Ok(group);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupExists(string id)
        {
            return db.Groups.Count(e => e.GroupId == id) > 0;
        }
    }
}