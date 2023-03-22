using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetAtlas2.Models;
using NetAtlas.ViewModels;
using System.Data.Entity;
using System.IO;

namespace NetAtlas2.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private int USEId = 0;
        private string IMG = string.Empty;
        //GET: User
        public ActionResult Index(int? a,string Img) 
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            ViewBag.USEId = a;
            ViewBag.IMG = Img;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(db.Users.Find(userId)); 
        }
        // Post: User/UpdatePicture
        [HttpPost]
        public ActionResult UpdatePicture(PictureVM obj)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            var file =obj.Picture;
            User user =db.Users.Find(userId);
            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string id_and_extension = userId + extension;
                string imgUrl = "~/Profile Images/" + id_and_extension;
                user.ImageUrl = imgUrl;
                db.Entry(user).State =EntityState.Modified;
                db.SaveChanges();
                var path = Server.MapPath("~/Profile Images/");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if((System.IO.File.Exists(path+id_and_extension)))
                {
                    System.IO.File.Delete(path+id_and_extension);
                }
                file.SaveAs(path+id_and_extension);
            }
            return RedirectToAction("Index");
        }
        //GET: User
       
        //GET: User
        public ActionResult ViewMore(int? id)
        {
            ViewBag.Model = db.Users.Find(5);
            return View();
        }
        
        
    }
}
