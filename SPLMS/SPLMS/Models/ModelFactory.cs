using AuthenticationAPI.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using AuthenticationAPI.Entities;

namespace AuthenticationAPI.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            List<string> roles = new List<string>();
            roles.Add(appUser.UserRole);
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Level = appUser.Level,
                JoinDate = appUser.JoinDate,
                //Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                Roles = roles,
                Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        public GroupReturnModel Create(Group group)
        {
            return new GroupReturnModel
            {
                Url = _UrlHelper.Link("GetGroupById", new { id = group.GroupId }),
                GroupId = group.GroupId,
                GroupName = group.GroupName,
                ProjectTopic = group.ProjectTopic,
                Year = group.Year,
                CourseCode = group.CourseCode,
                TeacherId = group.TeacherId
            };
        }

        public RuleReturnModel Create(GroupRule groupRule)
        {
            return new RuleReturnModel
            {
                Url = _UrlHelper.Link("GetRuleById", new {id = groupRule.RulesId}),
                RulesId = groupRule.RulesId,
                CommitteeId = groupRule.CommitteeId,
                CourseCode = groupRule.CourseCode,
                NumberOfStudentInEachGroup = groupRule.NumberOfStudentInEachGroup,
                MaxNumOfStudentOneCanSupervise = groupRule.MaxNumOfStudentOneCanSupervise
            };
        }

        public CalendarReturnModel Create(AcademicCalendar academicCalendar)
        {
            return new CalendarReturnModel
            {
                Url = _UrlHelper.Link("GetCalendarById", new { id = academicCalendar.CalendarId }),
                CalendarId = academicCalendar.CalendarId,
                CommitteeId = academicCalendar.CommitteeId,
                ProposalDate = academicCalendar.ProposalDate,
                MidtermDate = academicCalendar.MidtermDate,
                DraftReportSubmission = academicCalendar.DraftReportSubmission,
                FinalReportSubmission = academicCalendar.FinalReportSubmission,
                FinalPresentation = academicCalendar.FinalPresentation,
                CourseCode = academicCalendar.CourseCode,
                Year = academicCalendar.Year
            };
        }

        public CommitteeReturnModel Create(Committee committee)
        {
            return new CommitteeReturnModel
            {
                Url = _UrlHelper.Link("GetCommitteeById", new { id = committee.CommitteeId }),
                CommitteeId = committee.CommitteeId,
                TeacherId = committee.TeacherId,
                Year = committee.Year,
                CourseCode = committee.CourseCode,
                CommitteRole = committee.CommitteRole
                
            };
        }

        public NotificationReturnModel Create(Notification notification)
        {
            return new NotificationReturnModel
            {
                Url = _UrlHelper.Link("GetNotificationById", new { id = notification.NotificationId }),
                NotificationId = notification.NotificationId,
                SenderId = notification.SenderId,
                ReceiverId = notification.ReceiverId,
                NotificationTime = notification.NotificationTime,
                Notifications = notification.Notifications
            };
        }
        

        public FeedbackReturnModel Create(Feedback feedback)
        {
            return new FeedbackReturnModel
            {
                Url = _UrlHelper.Link("GetFeedbackById", new { id = feedback.FeedbackId }),
                FeedbackId = feedback.FeedbackId,
                GroupId = feedback.GroupId,
                TeacherId = feedback.TeacherId,
                Feedbacks = feedback.Feedbacks
            };
        }

        public RoleReturnModel Create(IdentityRole appRole)
        {

            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id }),
                Id = appRole.Id,
                Name = appRole.Name
            };

        }
    }

    public class RuleReturnModel
    {
        public string Url { get; set; }
        public string RulesId { get; set; }
        public int NumberOfStudentInEachGroup { get; set; }
        public int MaxNumOfStudentOneCanSupervise { get; set; }
        public string CommitteeId { get; set; }
        public string CourseCode { get; set; }
    }

    public class CommitteeReturnModel
    {
        public string Url { get; set; }
        public string CommitteeId { get; set; }
        public string TeacherId { get; set; }
        public int Year { get; set; }
        public string CourseCode { get; set; }
        public string CommitteRole { get; set; }
    }

    public class CalendarReturnModel
    {
        public string Url { get; set; }
        public string CalendarId { get; set; }
        public DateTime ProposalDate { get; set; }
        public DateTime MidtermDate { get; set; }
        public DateTime DraftReportSubmission { get; set; }
        public DateTime FinalReportSubmission { get; set; }
        public DateTime FinalPresentation { get; set; }
        public string CommitteeId { get; set; }
        public string CourseCode { get; set; }
        public int Year { get; set; }
    }

    public class NotificationReturnModel
    {
        public string Url { get; set; }
        public string NotificationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public DateTime NotificationTime { get; set; }
        public string Notifications { get; set; }
    }

    public class FeedbackReturnModel
    {
        public string Url { get; set; }

        public string FeedbackId { get; set; }

        public string GroupId { get; set; }

        public string TeacherId { get; set; }

        public string Feedbacks { get; set; }
    }

    public class GroupReturnModel
    {
        public string Url { get; set; }

        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public string ProjectTopic { get; set; }

        public int Year { get; set; }

        public string CourseCode { get; set; }

        public string TeacherId { get; set; }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}