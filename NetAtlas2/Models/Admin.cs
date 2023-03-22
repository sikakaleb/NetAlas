using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetAtlas2.Models
{
    public class Admin
    {
        [MaxLength(900),Column(TypeName = "VARCHAR"), Index(IsUnique = true)]
        public String Id { get; set; } = String.Empty;
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

        public static int nbre = 0;
        [MaxLength(1500), Column(TypeName = "VARCHAR")]
        public String Identity { get; set; } = String.Empty;

        public Admin(string userName, string password, string email, string imageUrl, DateTime? createdOn)
        {
            ++nbre;
            UserName = userName;
            Password = password;
            Email = email;
            ImageUrl = imageUrl;
            CreatedOn = createdOn;
            Identity = "ADMIN"+UserName.Substring(0,2)+Password.Substring(0,2)+Email.Substring(0,2)+nbre+"";
            Id = Identity;
        }
        public Admin()
        {
            ++nbre;
           
        }
    }
}