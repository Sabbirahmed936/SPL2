
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Committee
    {
        [Key]
        public string CommitteeId { get; set; }
        public string TeacherId { get; set; }
        public int Year { get; set; }
        public string CourseCode { get; set; }
        public string CommitteRole { get; set; }
        
    }
}
