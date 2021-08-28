
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        [Key]
        public string FeedbackId { get; set; }
        public string GroupId { get; set; }
        public string TeacherId { get; set; }
        public string Feedbacks { get; set; }
        
    }
}
