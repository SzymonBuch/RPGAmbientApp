using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Models
{
    public class Emails
    {
        [Key]
        public Guid Id { get; set; } 
        public string Email1 { get; set; }
        [DisplayName("Email address2")]
        [Required]
        [EmailAddress]
        public string Email2 { get; set; }
        public string Email3 { get; set; }
    }
}
