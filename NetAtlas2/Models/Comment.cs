using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetAtlas2.Models
{
    [Table("Comment")]
    public class Comment : Publication
    {
        
        [Required,DataType(DataType.MultilineText)]
        public string Text { get; set; } = String.Empty;
        [MaxLength(500), Column(TypeName = "VARCHAR")]
        public static string type { get; set; } = "Comment";
    }
}