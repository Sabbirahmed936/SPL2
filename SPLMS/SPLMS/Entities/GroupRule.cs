using System.ComponentModel.DataAnnotations;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class GroupRule
    {
        [Key]
        public string RulesId { get; set; }
		public int NumberOfStudentInEachGroup { get; set; }
		public int MaxNumOfStudentOneCanSupervise { get; set; }
        public string CommitteeId { get; set; }
        public string CourseCode { get; set; }
        
    
    }
}