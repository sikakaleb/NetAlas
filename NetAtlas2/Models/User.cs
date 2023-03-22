using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetAtlas2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(5), MaxLength(500), Index(IsUnique = true), Column(TypeName = "VARCHAR")]
        public string UserName { get; set; } = String.Empty;
        [Required, MinLength(5), MaxLength(500), DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        [EmailAddress, Index(IsUnique = true), Column(TypeName = "VARCHAR"), Required]
        public string Email { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        [Display(Name = "Picture")]
        public string ImageUrl { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }
        [MaxLength(8000), Column(TypeName = "VARCHAR")]
        public string AllFriend { get; set; } = String.Empty;

        [MaxLength(8000), Column(TypeName = "VARCHAR")]
        public string Invitation { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        public int Advice { get; set; } = 0;
        [ScaffoldColumn(false)]
        public Boolean Status { get; set; } = false;
        [ScaffoldColumn(false), Column(TypeName = "VARCHAR")]
        public string FullName { get; set; } = String.Empty;
        [ScaffoldColumn(false), Column(TypeName = "VARCHAR")]
        public string Country { get; set; } = String.Empty;
        [ScaffoldColumn(false)]
        public DateTime? DatNaiss { get; set; }
        [ScaffoldColumn(false), Column(TypeName = "VARCHAR")]
        public string Bio { get; set; } = String.Empty;




        /* public void UpdateFriend(int myid,int id)
         {
             string user = db.Users.Find(id).AllFriend + $"{id}";
             db.Users.Find(myid).AllFriend = user;
             db.SaveChanges();

         }*/

    }
}