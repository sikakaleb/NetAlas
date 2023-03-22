using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetAtlas2.Models
{
    [Table("Photo")]
    public class Photo : Publication
    {
      
        [Required]
        public string Phot_Picture { get; set; }

        [MaxLength(500), Column(TypeName = "VARCHAR")]
        public static string type { get; set; } = "Photo";
    }
}