using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetAtlas2.Models
{
    public class Publication
    {

        [Key]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public ICollection<Reply> Replies { get; set; }
        [ScaffoldColumn(false)]
        public Boolean Status { get; set; } = false;

        [MaxLength(5000), Column(TypeName = "VARCHAR")]
        public  string titre { get; set; } 
    }
}