
using System.ComponentModel.DataAnnotations;
using AuthenticationAPI.Infrastructure;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        [Key]
        public string Id { get; set; }
        public string Designation { get; set; }
    }
}
