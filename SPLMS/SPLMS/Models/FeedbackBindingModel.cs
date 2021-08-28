using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class FeedbackBindingModel
    {

        [Required]
        [Display(Name = "GroupId")]
        public string GroupId { get; set; }

        [Required]
        [Display(Name = "TeacherId")]
        public string TeacherId { get; set; }

        [Required]
        [Display(Name = "Feedbacks")]
        public string Feedbacks { get; set; }
    }
}