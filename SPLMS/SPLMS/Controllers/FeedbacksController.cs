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
    [RoutePrefix("api/feedbacks")]
    public class FeedbacksController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Feedbacks
        [Route("all")]
        public IQueryable<Feedback> GetFeedbacks()
        {
            return db.Feedbacks;
        }
        [Route("group/{id}")]
        public IQueryable<Feedback> GetFeedbacksByGroupId(string id)
        {
            return from feedback in db.Feedbacks where feedback.GroupId == id select feedback;
        }

        // GET: api/Feedbacks/5
        [Route("{id}", Name = "GetFeedbackById")]
        public async Task<IHttpActionResult> GetFeedback(string id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(feedback));
        }

        //[Route("{GroupId}", Name = "GetFeedbackByGroupId")]
        //public List<string> GetFeedbackByGroupId(string id)
        //{
        //    String feedbacks = db.Feedbacks.FirstOrDefault(t => t.GroupId == id).Feedbacks.ToString();
        //    if (feedbacks == null)
        //    {
        //        return null;
        //    }

        //    return feedbacks;
        //}

        // PUT: api/Feedbacks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedback(string id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }

            db.Entry(feedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedbacks
        [Route("create")]
        [ResponseType(typeof(Feedback))]
        public async Task<IHttpActionResult> PostFeedback(FeedbackBindingModel feedbackBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var feedback = new Feedback()
            {
                FeedbackId = feedbackBindingModel.GroupId+feedbackBindingModel.TeacherId,
                GroupId = feedbackBindingModel.GroupId,
                TeacherId = feedbackBindingModel.TeacherId,
                Feedbacks = feedbackBindingModel.Feedbacks
            };

            db.Feedbacks.Add(feedback);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FeedbackExists(feedback.FeedbackId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var locationHeader = new Uri(Url.Link("GetFeedbackById", new { id = feedback.FeedbackId }));
            return Created(locationHeader, TheModelFactory.Create(feedback));
        }

        // DELETE: api/Feedbacks/5
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult DeleteFeedback(string id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return NotFound();
            }

            db.Feedbacks.Remove(feedback);
            db.SaveChanges();

            return Ok(feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(string id)
        {
            return db.Feedbacks.Count(e => e.FeedbackId == id) > 0;
        }
    }
}