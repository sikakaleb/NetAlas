using NetAtlas2.Models;
using NetAtlas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetAtlas2.ViewModels;
using System.Net.Mail;

namespace NetAtlas2.Controllers
{
    public class AccountController : Controller
    {
        public  int UserIDI;
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET :Account/Register
        public static void CreateMail(User user)
        {
            MailAddress to = new MailAddress(user.Email);
            MailAddress from = new MailAddress("kalebsika22@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Wait for account activation";
            message.Body = @"We have correctly received your registration messsage for the social network.we are actively working to satisfy you. Please Mr. and Mrs. wait a moment";
            // Use the application or machine configuration to get the
            // host, port, and credentials.
            SmtpClient client = new SmtpClient();
            Console.WriteLine("Sending an email message to {0} at {1} by using the SMTP host={2}.",
                to.User, to.Host, client.Host);
            client.Send(message);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //GET :Account/Register
        [HttpPost]
        public ActionResult Register(RegisterVM obj,string Birthday)
        {
            int Usercount = db.Users.Count();
            if (Usercount == 0)
            {
                User user = new User();
                user.FullName = obj.FullName;
                user.UserName = obj.userName;
                user.DatNaiss = DateTime.Parse(Birthday);
                user.Country = obj.Country;
                user.Email = obj.Email;
                user.Password = obj.Password;
                user.Bio=obj.Bio;
                user.ImageUrl = "";
                //user.
                DateTime now = DateTime.Now;
                user.CreatedOn = now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                bool UserExistis = db.Users.Any(x => x.UserName == obj.userName);
                if (UserExistis)//If UserName is already in use
                {
                    ViewBag.UserNameMessage = "This UserName is already in use, try another";
                    return View();

                }
                bool EmailExistis = db.Users.Any(y => y.Email == obj.Email);
                if (EmailExistis)
                {
                    ViewBag.EmailMessage = "This Email is already in use, try another";
                    return View();
                }
                User user = new User();
                user.FullName = obj.FullName;
                user.UserName = obj.userName;
                user.DatNaiss = DateTime.Parse(Birthday);
                user.Country = obj.Country;
                user.Email = obj.Email;
                user.Password = obj.Password;
                user.Bio = obj.Bio;
                user.ImageUrl = "";
                DateTime now = DateTime.Now;
                user.CreatedOn = now;
                db.Users.Add(user);
                db.SaveChanges();
                
                return RedirectToAction("Login", "Account");
            }
           
            
            //if UserName and emai l is unique , then we save or register the user
            
            return View();
            
        }
        //GET :Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM obj)
        {

            bool exists = db.Users.Any(x => x.UserName == obj.Username &&
            x.Password == obj.Password &&x.Status==true);
            if (exists)
            {
                var personId = db.Users.Single(x => x.UserName == obj.Username && x.Status == true).Id;
                Session["UseId"] = db.Users.Single(x => x.UserName == obj.Username && x.Status == true).Id;
                Session["ImageUrl"] = db.Users.Single(x => x.UserName == obj.Username && x.Status == true).ImageUrl;
                return RedirectToAction("Index", "ChatRoom", new { a = Convert.ToInt32(Session["UseId"]), Img = Session["ImageUrl"].ToString() });

            }
            //if invalid credentials
            ViewBag.Message = "Invalid Credentials!";
            return View();
        }
        //GET :Account/Logout
        [HttpGet]
        public ActionResult Logout()

        {
            Session["UseId"]= 0;
            UserIDI = 0;
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult HLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HLogin(HLoginVM obj)
        {
            bool isAdmin = false;
            bool isMod = false;
            

            isAdmin = db.Admins.Any(x => x.UserName == obj.Username && x.Password == obj.Password && x.Email == obj.Email);
            isMod = db.Moderateurs.Any(x => x.UserName == obj.Username && x.Password == obj.Password && x.Email == obj.Email);
            ViewBag.test = "none";
            if (isAdmin == true || isMod==true)
            {
                ViewBag.test = "admin";
                //ViewBag.AdminId = db.Admins.Single(x => x.UserName == obj.Username && x.Email == obj.Email).Identity;

                var AdminId = db.Admins.Where(x => x.UserName == obj.Username && x.Email == obj.Email).First();
                string idadm = AdminId.Id.ToString();
                /*if (isMod==false)
                {
                    var ModId = db.Moderateurs.Where(x => x.UserName == obj.Username && x.Email == obj.Email).First(); ;
                    string idmod = ModId.Id.ToString();
                }*/
              
                ViewBag.test = idadm;
                if (idadm.ToString().Substring(0,5) == "ADMIN")
                {
                    ViewBag.test = "admin_success";
                    Session["AdministrateurId"] = idadm;
                    Session["ImageUrl"] = db.Admins.Single(x => x.UserName == obj.Username &&
                                x.Password == obj.Password && x.Email == obj.Email).ImageUrl;
                    return RedirectToAction("Index", "Admin", new { a = Session["AdministrateurId"].ToString(), Img= Session["ImageUrl"].ToString() });
                }
                if (idadm.Substring(0, 5) == "MODER")
                {
                    Session["ModerateurId"] = idadm;
                    Session["ImageUrl"] = db.Moderateurs.Single(x => x.UserName == obj.Username &&
                                x.Password == obj.Password && x.Email == obj.Email).ImageUrl;
                    return RedirectToAction("Index", "Moderateur", new { a = Session["ModerateurId"].ToString(), Img = Session["ImageUrl"].ToString() });
                }
            }
            
            

            //if invalid credentials*/
            ViewBag.Message = "Invalid Credentials!";
            return View();
        }

        
    }
}
