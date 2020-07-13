using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.PostGre
{
    public partial class Bus
    {
        public Bus()
        {
            Passanger = new HashSet<Passanger>();
        }

        public int BusId { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string BusName { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string BusType { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string BusNumber { get; set; }
        public int DriversId { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime StartTimeStamp { get; set; }
        public bool IsEnd { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime? EndTimeStamp { get; set; }

        [ForeignKey("DriversId")]
        [InverseProperty("Bus")]
        public virtual Drivers Drivers { get; set; }
        [InverseProperty("Bus")]
        public virtual ICollection<Passanger> Passanger { get; set; }
    }
}
