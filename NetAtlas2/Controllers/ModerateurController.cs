using NetAtlas.ViewModels;
using NetAtlas2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetAtlas2.Controllers
{
    public class ModerateurController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Moderateur
        public string ModerateurId = null;
        public ActionResult Index(string a,string Img)
        {
            ViewBag.Mod = Session["ModerateurId"];
            string ModerateurId =a;
            if (ModerateurId == null)
            {
                return RedirectToAction("HLogin", "Account");
            }
            var comments = db.Comments.Where(x => x.Status == true && x.User.Status==true).OrderByDescending(x => x.CreatedOn).ToList();
            return View(comments);
        }
        // GET: Admin

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM obj)
        {
            bool UserExistis = db.Moderateurs.Any(x => x.UserName == obj.userName);
            if (UserExistis)//If UserName is already in use
            {
                ViewBag.UserNameMessage = "This UserName is already in use, try another";
                return View();

            }
            bool EmailExistis = db.Moderateurs.Any(y => y.Email == obj.Email);
            if (EmailExistis)
            {
                ViewBag.EmailMessage = "This Email is already in use, try another";
                return View();
            }
            //if UserName and emai l is unique , then we save or register the user

            string UserName = obj.userName;
            string Password = obj.Password;
            string Email = obj.Email;
            string ImageUrl = "";
            DateTime now = DateTime.Now;
            Moderateur user = new Moderateur(UserName, Password, Email, ImageUrl, now);
            db.Moderateurs.Add(user);
            db.SaveChanges();
            return RedirectToAction("HLogin", "Account");
        }

        public ActionResult ListUser()
        {
            
            if (db.Users.Count() != 0)
            {
                ViewBag.Search = "Ah you have friend baka";
                 var users = db.Users.ToList().OrderByDescending(x => x.CreatedOn).ToList();
                return View(users);
            }
            ViewBag.Search = "No Friend you are just lonely";
            return View();
           
        }

    }
}