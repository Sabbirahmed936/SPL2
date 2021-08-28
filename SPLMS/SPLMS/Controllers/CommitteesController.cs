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
    [RoutePrefix("api/committees")]
    public class CommitteesController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Committees
        public IQueryable<Committee> GetCommittees()
        {
            return db.Committees;
        }

        // GET: api/Committees/5
        [ResponseType(typeof(Committee))]
        [Route("{id}", Name = "GetCommitteeById")]
        public async Task<IHttpActionResult> GetCommittee(string id)
        {
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(committee));
        }

        // PUT: api/committees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommittee(string id, Committee committee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != committee.CommitteeId)
            {
                return BadRequest();
            }

            db.Entry(committee).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommitteeExists(id))
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

        // POST: api/committees
        [Route("create")]
        [ResponseType(typeof(Committee))]
        public IHttpActionResult PostCommittee(CommitteeBindingModel committeeBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var committee = new Committee()
            {
                CommitteeId = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString(),
                TeacherId = committeeBindingModel.TeacherId,
                Year = committeeBindingModel.Year,
                CourseCode = committeeBindingModel.CourseCode,
                CommitteRole = "Manager"//committeeBindingModel.CommitteRole
            };

            db.Committees.Add(committee);

            var user = db.Users.FirstOrDefault(u => u.Id == committeeBindingModel.TeacherId);
            user.UserRole = "Manager";

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CommitteeExists(committee.CommitteeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var locationHeader = new Uri(Url.Link("GetCommitteeById", new { id = committee.CommitteeId }));
            return Created(locationHeader, TheModelFactory.Create(committee)); ;
        }

        // DELETE: api/Committees/5
        [ResponseType(typeof(Committee))]
        public IHttpActionResult DeleteCommittee(string id)
        {
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return NotFound();
            }

            db.Committees.Remove(committee);
            db.SaveChanges();

            return Ok(committee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommitteeExists(string id)
        {
            return db.Committees.Count(e => e.CommitteeId == id) > 0;
        }
    }
}