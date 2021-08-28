
using System.ComponentModel.DataAnnotations;
using AuthenticationAPI.Infrastructure;

namespace AuthenticationAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Document
    {
        [Key]
        public string DocumentId { get; set; }
        public string UserId { get; set; }
        public string GroupId { get; set; }
		public string DocumentCategory { get; set; }
        public string StorePath { get; set; }
        
    
    }
}
