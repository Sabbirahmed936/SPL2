
using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Group
    {
        [Key]
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string ProjectTopic { get; set; }
        public int Year { get; set; }
        public string TeacherId { get; set; }
        public string CourseCode { get; set; }
        
    }
}
