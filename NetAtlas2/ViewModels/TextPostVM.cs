using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetAtlas2.ViewModels
{
    public class TextPostVM
    {
        [Required]
        public string text { get; set; }
        [Required]
        public string titre { get; set; }
        [Required]
        public string type { get; set; }
    }
}