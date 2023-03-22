using System.ComponentModel.DataAnnotations;
using System.Web;
namespace NetAtlas.ViewModels
{
    public class PictureVM 
    {
        [Required]
        public HttpPostedFileWrapper Picture { get; set; }
    }
}
