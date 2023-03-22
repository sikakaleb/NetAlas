using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NetAtlas2.Models
{
    [Table("Moderateur")]
    public class Moderateur : Admin
    {

        public static int nbremod { get; set; } = 0;
        public Moderateur(string userName, string password, string email, string imageUrl, DateTime? createdOn) 
        {
            ++nbremod;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.ImageUrl = imageUrl;
            this.CreatedOn = createdOn;
            this.Identity = "MODER" + userName.Substring(0,2) + password.Substring(0,2) + Email.Substring(0,2)+$"{nbremod}";
            this.Id = this.Identity;
        }

        public Moderateur()
        {
            ++nbremod;
        }
    }
}