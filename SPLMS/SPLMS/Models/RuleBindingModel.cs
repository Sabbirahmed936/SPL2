using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class RuleBindingModel
    {
        [Display(Name = "RulesId")]
        public string RulesId { get; set; }
        [Required]
        [Display(Name = "NumberOfStudentInEachGroup")]
        public int NumberOfStudentInEachGroup { get; set; }
        [Required]
        [Display(Name = "MaxNumOfStudentOneCanSupervise")]
        public int MaxNumOfStudentOneCanSupervise { get; set; }
        [Required]
        [Display(Name = "CommitteeId")]
        public string CommitteeId { get; set; }
        [Required]
        [Display(Name = "CourseCode")]
        public string CourseCode { get; set; }
    }
}