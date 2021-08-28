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
    [RoutePrefix("api/notifications")]
    public class NotificationsController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Notifications
        public IQueryable<Notification> GetNotifications()
        {
            return db.Notifications;
        }

        [Route("{id}", Name = "GetNotificationById")]
        public async Task<IHttpActionResult> GetNotification(string id)
        {
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(notification));
        }

        // PUT: api/Notifications/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotification(string id, Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.NotificationId)
            {
                return BadRequest();
            }

            db.Entry(notification).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        [Route("create")]
        [ResponseType(typeof(Notification))]
        public IHttpActionResult PostNotification(NotificationBindingModel notificationBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notification = new Notification()
            {
                NotificationId = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString(),
                SenderId = notificationBindingModel.SenderId,
                ReceiverId = notificationBindingModel.ReceiverId,
                NotificationTime = DateTime.Now,
                Notifications = notificationBindingModel.Notifications
            };

            db.Notifications.Add(notification);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NotificationExists(notification.NotificationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var locationHeader = new Uri(Url.Link("GetNotificationById", new { id = notification.NotificationId }));
            return Created(locationHeader, TheModelFactory.Create(notification));
        }

        // DELETE: api/Notifications/5
        [ResponseType(typeof(Notification))]
        public IHttpActionResult DeleteNotification(string id)
        {
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.Notifications.Remove(notification);
            db.SaveChanges();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationExists(string id)
        {
            return db.Notifications.Count(e => e.NotificationId == id) > 0;
        }
    }
}