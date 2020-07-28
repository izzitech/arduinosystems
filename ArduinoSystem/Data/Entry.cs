using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArduinoSystem.Data
{
    public class Entry
    {
        public int Id { get; set; }

        [ForeignKey("Channel")]
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }

        [Display(Name="Date of creation")]
        public DateTime CreatedAt { get; set; }
        public double? Field1 { get; set; }
        public double? Field2 { get; set; }
        public double? Field3 { get; set; }
        public double? Field4 { get; set; }
        public double? Field5 { get; set; }
        public double? Field6 { get; set; }
        public double? Field7 { get; set; }
        public double? Field8 { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Elevation { get; set; }
        public string Location { get; set; }
    }
}
