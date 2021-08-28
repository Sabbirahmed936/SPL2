
using System.ComponentModel.DataAnnotations;
using AuthenticationAPI.Infrastructure;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {
        [Key]
        public string NotificationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public System.DateTime NotificationTime { get; set; }
        public string Notifications { get; set; }
    }
}
