
using System.ComponentModel.DataAnnotations;
using AuthenticationAPI.Infrastructure;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        [Key]
        public string Id { get; set; }
        public int RollNo { get; set; }
        public int Batch { get; set; }
        public string GroupId { get; set; }
    
    }
}
