using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AuthenticationAPI.Entities;
using AuthenticationAPI.Infrastructure;
using AuthenticationAPI.Models;

namespace AuthenticationAPI.Controllers
{
    [RoutePrefix(("api/grouprules"))]
    public class GroupRulesController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/GroupRules
        public IQueryable<GroupRule> GetGroupRules()
        {
            return db.GroupRules;
        }

        // GET: api/GroupRules/5
        [Route("{id}", Name = "GetRuleById")]
        [ResponseType(typeof(GroupRule))]
        public IHttpActionResult GetGroupRule(string id)
        {
            GroupRule groupRule = db.GroupRules.Find(id);
            if (groupRule == null)
            {
                return NotFound();
            }

            return Ok(this.TheModelFactory.Create(groupRule));
        }

        // PUT: api/GroupRules/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGroupRule(string id, GroupRule groupRule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != groupRule.RulesId)
            {
                return BadRequest();
            }

            db.Entry(groupRule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupRuleExists(id))
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

        // POST: api/GroupRules
        [ResponseType(typeof(GroupRule))]
        [Route("create")]
        public IHttpActionResult PostGroupRule(RuleBindingModel ruleBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var groupRule = new GroupRule()
            {
                RulesId = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString(),
                CommitteeId = ruleBindingModel.CommitteeId,
                NumberOfStudentInEachGroup = ruleBindingModel.NumberOfStudentInEachGroup,
                MaxNumOfStudentOneCanSupervise = ruleBindingModel.MaxNumOfStudentOneCanSupervise,
                CourseCode = ruleBindingModel.CourseCode
            };

            db.GroupRules.Add(groupRule);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GroupRuleExists(groupRule.RulesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var locationHeader = new Uri(Url.Link("GetRuleById", new { id = groupRule.RulesId }));
            return Created(locationHeader, TheModelFactory.Create(groupRule));
        }

        // DELETE: api/GroupRules/5
        [ResponseType(typeof(GroupRule))]
        public IHttpActionResult DeleteGroupRule(string id)
        {
            GroupRule groupRule = db.GroupRules.Find(id);
            if (groupRule == null)
            {
                return NotFound();
            }

            db.GroupRules.Remove(groupRule);
            db.SaveChanges();

            return Ok(groupRule);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroupRuleExists(string id)
        {
            return db.GroupRules.Count(e => e.RulesId == id) > 0;
        }
    }
}