using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArduinoSystem.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(140, MinimumLength = 2)]
        public string Name { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
