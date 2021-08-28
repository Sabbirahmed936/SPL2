using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class CommitteeBindingModel
    {
        [Display(Name = "CommitteeId")]
        public string CommitteeId { get; set; }
        [Required]
        [Display(Name = "TeacherId")]
        public string TeacherId { get; set; }
        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "CourseCode")]
        public string CourseCode { get; set; }
        
        [Display(Name = "CommitteeRole")]
        public string CommitteRole { get; set; }
    }
}