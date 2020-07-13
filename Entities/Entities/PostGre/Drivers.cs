using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.PostGre
{
    public partial class Drivers
    {
        public Drivers()
        {
            Bus = new HashSet<Bus>();
        }

        public int DriversId { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string DriversName { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string DriversPhoneNumber { get; set; }

        [InverseProperty("Drivers")]
        public virtual ICollection<Bus> Bus { get; set; }
    }
}
