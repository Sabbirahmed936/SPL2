using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationAPI.Models
{
    public class NotificationBindingModel
    {
        
        [Required]
        [Display(Name = "SenderId")]
        public string SenderId { get; set; }
        [Required]
        [Display(Name = "ReceiverId")]
        public string ReceiverId { get; set; }

        public DateTime NotificationTime { get; set; }
        [Required]
        [Display(Name = "Notifications")]
        public string Notifications { get; set; }
    }
}