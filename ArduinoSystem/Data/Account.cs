using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoSystem.Data
{
    public class Account
    {
        public Guid Id { get; set; }
        
        [StringLength(140, MinimumLength = 2)]
        public string Name { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
