using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AuthenticationAPI.Entities;

namespace AuthenticationAPI.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection" , throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Committee> Committees { get; set; }
        public virtual DbSet<AcademicCalendar> AcademicCalendars { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<GroupRule> GroupRules { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Resubmission> Resubmissions { get; set; }
        
    }
}