
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class AcademicCalendar
    {
        [Key]
        public string CalendarId { get; set; }
        public System.DateTime ProposalDate { get; set; }
        public System.DateTime MidtermDate { get; set; }
        public System.DateTime DraftReportSubmission { get; set; }
        public System.DateTime FinalReportSubmission { get; set; }
        public System.DateTime FinalPresentation { get; set; }
        public string CommitteeId { get; set; }
        public int Year { get; set; }
        public string CourseCode { get; set; }
        
    }
}
