using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class CalendarBindingModel
    {
        [Display(Name = "CalendarId")]
        public string CalendarId { get; set; }
        [Required]
        [Display(Name = "ProposalDate")]
        public System.DateTime ProposalDate { get; set; }
        [Required]
        [Display(Name = "MidtermDate")]
        public System.DateTime MidtermDate { get; set; }
        [Required]
        [Display(Name = "DraftReportSubmission")]
        public System.DateTime DraftReportSubmission { get; set; }
        [Required]
        [Display(Name = "FinalReportSubmission")]
        public System.DateTime FinalReportSubmission { get; set; }
        [Required]
        [Display(Name = "FinalPresentation")]
        public System.DateTime FinalPresentation { get; set; }
        [Required]
        [Display(Name = "CommitteeId")]
        public string CommitteeId { get; set; }
        [Required]
        [Display(Name = "CourseCode")]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}