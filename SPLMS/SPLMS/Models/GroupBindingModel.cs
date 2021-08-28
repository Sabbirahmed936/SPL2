using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class GroupBindingModel
    {
        [Required]
        [Display(Name = "GroupName")]
        public string GroupName { get; set; }
        [Required]
        [Display(Name = "ProjectTopic")]
        public string ProjectTopic { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "TeacherId")]
        public string TeacherId { get; set; }
        [Required]
        [Display(Name = "CourseCode")]
        public string CourseCode { get; set; }

        //[Display(Name = "StudentId1")]
        //public string StudentId1 { get; set; }

        //[Display(Name = "StudentId2")]
        //public string StudentId2 { get; set; }
    }
}