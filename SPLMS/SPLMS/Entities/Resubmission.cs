
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resubmission
    {
        [Key]
        public string Id { get; set; }
        public int Year { get; set; }
        public string CommitteeId { get; set; }
        public string GroupId { get; set; }
        public string Feedback { get; set; }
        public System.DateTime ResubmissionDate { get; set; }
        
    
    }
}
