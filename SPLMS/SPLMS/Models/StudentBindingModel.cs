using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class StudentBindingModel
    {

        [Required]
        [Display(Name ="GroupId")]
        public string GroupId { get; set; }
        [Display(Name = "Roll")]
        public int Roll { get; set; }

        [Display(Name = "Batch")]
        public int Batch { get; set; }
    }
}