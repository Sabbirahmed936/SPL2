namespace AuthenticationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicCalendars",
                c => new
                    {
                        CalendarId = c.String(nullable: false, maxLength: 128),
                        ProposalDate = c.DateTime(nullable: false),
                        MidtermDate = c.DateTime(nullable: false),
                        DraftReportSubmission = c.DateTime(nullable: false),
                        FinalReportSubmission = c.DateTime(nullable: false),
                        FinalPresentation = c.DateTime(nullable: false),
                        CommitteeId = c.String(),
                        Year = c.Int(nullable: false),
                        CourseCode = c.String(),
                    })
                .PrimaryKey(t => t.CalendarId);
            
            CreateTable(
                "dbo.Committees",
                c => new
                    {
                        CommitteeId = c.String(nullable: false, maxLength: 128),
                        TeacherId = c.String(),
                        Year = c.Int(nullable: false),
                        CourseCode = c.String(),
                        CommitteRole = c.String(),
                    })
                .PrimaryKey(t => t.CommitteeId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        GroupId = c.String(),
                        DocumentCategory = c.String(),
                        StorePath = c.String(),
                    })
                .PrimaryKey(t => t.DocumentId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.String(),
                        TeacherId = c.String(),
                        Feedbacks = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId);
            
            CreateTable(
                "dbo.GroupRules",
                c => new
                    {
                        RulesId = c.String(nullable: false, maxLength: 128),
                        NumberOfStudentInEachGroup = c.Int(nullable: false),
                        MaxNumOfStudentOneCanSupervise = c.Int(nullable: false),
                        CommitteeId = c.String(),
                        CourseCode = c.String(),
                    })
                .PrimaryKey(t => t.RulesId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        GroupName = c.String(),
                        ProjectTopic = c.String(),
                        Year = c.Int(nullable: false),
                        TeacherId = c.String(),
                        CourseCode = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.String(nullable: false, maxLength: 128),
                        SenderId = c.String(),
                        ReceiverId = c.String(),
                        NotificationTime = c.DateTime(nullable: false),
                        Notifications = c.String(),
                    })
                .PrimaryKey(t => t.NotificationId);
            
            CreateTable(
                "dbo.Resubmissions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Year = c.Int(nullable: false),
                        CommitteeId = c.String(),
                        GroupId = c.String(),
                        Feedback = c.String(),
                        ResubmissionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RollNo = c.Int(nullable: false),
                        Batch = c.Int(nullable: false),
                        GroupId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Designation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        UserRole = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Resubmissions");
            DropTable("dbo.Notifications");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupRules");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Documents");
            DropTable("dbo.Committees");
            DropTable("dbo.AcademicCalendars");
        }
    }
}
