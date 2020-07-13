using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities.PostGre
{
    public partial class Tokens
    {
        public int TokenId { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string TokenType { get; set; }
        [Required]
        [Column(TypeName = "character varying")]
        public string UniqueCode { get; set; }
        public bool IsExpired { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TimeStamp { get; set; }
    }
}
