using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoSystem.Data
{
    public class Channel
    {
        [Display(Name = "ApiKey")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(140, MinimumLength = 2)]
        public string Name { get; set; }

        [ForeignKey("Acount")]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }

        [StringLength(140)]
        [Display(Name="Field1")]
        public string Field1_Name { get; set; }
        
        [StringLength(140)]
        [Display(Name = "Field2")]
        public string Field2_Name { get; set; }

        [StringLength(140)]
        [Display(Name = "Field3")]
        public string Field3_Name { get; set; }
        
        [StringLength(140)]
        [Display(Name = "Field4")]
        public string Field4_Name { get; set; }

        [StringLength(140)]
        [Display(Name = "Field5")]
        public string Field5_Name { get; set; }
        
        [StringLength(140)]
        [Display(Name = "Field6")]
        public string Field6_Name { get; set; }
        
        [StringLength(140)]
        [Display(Name = "Field7")]
        public string Field7_Name { get; set; }
        
        [StringLength(140)]
        [Display(Name = "Field8")]
        public string Field8_Name { get; set; }
    }
}
