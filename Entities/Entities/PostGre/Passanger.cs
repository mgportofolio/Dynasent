using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.PostGre
{
    public partial class Passanger
    {
        public int PassangerId { get; set; }
        public int BusId { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string PassangerName { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string PassangerContact { get; set; }
        public bool InOrOut { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TimeStamp { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string QrCodeSrc { get; set; }

        [ForeignKey("BusId")]
        [InverseProperty("Passanger")]
        public virtual Bus Bus { get; set; }
    }
}
