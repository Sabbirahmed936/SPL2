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
    [RoutePrefix("api/calendars")]
    public class AcademicCalendarsController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AcademicCalendars
        [Route("all")]
        public IQueryable<AcademicCalendar> GetAcademicCalendars()
        {
            return db.AcademicCalendars;
        }

        // GET: api/AcademicCalendars/5
        [Route("{id}", Name = "GetCalendarById")]
        public async Task<IHttpActionResult> GetCalendarById(string id)
        {
            AcademicCalendar calendar = db.AcademicCalendars.Find(id);
            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(calendar));
        }

        // PUT: api/AcademicCalendars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAcademicCalendar(string id, AcademicCalendar academicCalendar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != academicCalendar.CalendarId)
            {
                return BadRequest();
            }

            db.Entry(academicCalendar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcademicCalendarExists(id))
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

        // POST: api/AcademicCalendars
        [Route("create")]
        [ResponseType(typeof(AcademicCalendar))]
        public IHttpActionResult PostAcademicCalendar(CalendarBindingModel calendarBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var academicCalendar = new AcademicCalendar()
            {
                CalendarId = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString(),
                CommitteeId = calendarBindingModel.CommitteeId,
                ProposalDate = calendarBindingModel.ProposalDate,
                MidtermDate = calendarBindingModel.MidtermDate,
                DraftReportSubmission = calendarBindingModel.DraftReportSubmission,
                FinalReportSubmission = calendarBindingModel.FinalReportSubmission,
                FinalPresentation = calendarBindingModel.FinalPresentation,
                CourseCode = calendarBindingModel.CourseCode,
                Year = calendarBindingModel.Year
            };

            db.AcademicCalendars.Add(academicCalendar);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AcademicCalendarExists(academicCalendar.CalendarId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var locationHeader = new Uri(Url.Link("GetCalendarById", new { id = academicCalendar.CalendarId }));
            return Created(locationHeader, TheModelFactory.Create(academicCalendar));
        }

        // DELETE: api/AcademicCalendars/5
        [ResponseType(typeof(AcademicCalendar))]
        public IHttpActionResult DeleteAcademicCalendar(string id)
        {
            AcademicCalendar academicCalendar = db.AcademicCalendars.Find(id);
            if (academicCalendar == null)
            {
                return NotFound();
            }

            db.AcademicCalendars.Remove(academicCalendar);
            db.SaveChanges();

            return Ok(academicCalendar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcademicCalendarExists(string id)
        {
            return db.AcademicCalendars.Count(e => e.CalendarId == id) > 0;
        }
    }
}