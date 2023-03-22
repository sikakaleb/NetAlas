using NetAtlas.ViewModels;
using NetAtlas2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NetAtlas2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db = new ApplicationDbContext();
        //********************mdp***************************//
        private static readonly Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // les charactères possibles dans la chaîne
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //*********************send mail********************//
        public static void ActiveAccount(User user)
        {
            MailAddress to = new MailAddress(user.Email);
            MailAddress from = new MailAddress("kalebsika22@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = " account activate";
            message.Body = @"Thank you for waiting your account has been correctly activated, you can start enjoying this wonder that we offer you";
            // Use the application or machine configuration to get the
            // host, port, and credentials.
            SmtpClient client = new SmtpClient();
            Console.WriteLine("Sending an email message to {0} at {1} by using the SMTP host={2}.",
                to.User, to.Host, client.Host);
            client.Send(message);
        }
        //*********************renitialise***************************//
        public static void RAccount(User user,string password)
        {
            MailAddress to = new MailAddress(user.Email);
            MailAddress from = new MailAddress("kalebsika22@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = " Renitialise activate";
            message.Body = @"your account was renitialised, yours new password is :"+ password;
            // Use the application or machine configuration to get the
            // host, port, and credentials.
            SmtpClient client = new SmtpClient();
            Console.WriteLine("Sending an email message to {0} at {1} by using the SMTP host={2}.",
                to.User, to.Host, client.Host);
            client.Send(message);
        }
        //**********************advice*******************************************//
        public static void Advice(Publication pub)
        {
            MailAddress to = new MailAddress(pub.User.Email);
            MailAddress from = new MailAddress("kalebsika22@gmail.com");
            MailMessage message = new MailMessage(from, to);
            message.Subject = " account activate";
            message.Body = @"you have received a garment, please no longer display publications degrading to the risk that your account is deleted idmessage"+pub.Id+""+"date de publication"+pub.CreatedOn+"";
            // Use the application or machine configuration to get the
            // host, port, and credentials.
            SmtpClient client = new SmtpClient();
            Console.WriteLine("Sending an email message to {0} at {1} by using the SMTP host={2}.",
                to.User, to.Host, client.Host);
            client.Send(message);
        }
        //*************index***********************************//
        public ActionResult Index(string a, string Img)
        {
            ViewBag.Mod = Session["AdministrateurId"];
            string ModerateurId = a;
            if (ModerateurId == null)
            {
                return RedirectToAction("HLogin", "Account");
            }
            var comments = db.Comments.Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList();
            return View(comments);
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM obj)
        {
            bool UserExistis = db.Admins.Any(x => x.UserName == obj.userName);
            if (UserExistis)//If UserName is already in use
            {
                ViewBag.UserNameMessage = "This UserName is already in use, try another";
                return View();

            }
            bool EmailExistis = db.Admins.Any(y => y.Email == obj.Email);
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
            Admin user = new Admin(UserName, Password, Email, ImageUrl, now);
            db.Admins.Add(user);
            db.SaveChanges();
            return RedirectToAction("HLogin", "Account");
        }

        public ActionResult AddMenbers(int id)
        {
            if(db.Users.Find(id).Status == false)
            {
                db.Users.Find(id).Status = true;
            }
            db.SaveChanges();
            return View();
        }

        public ActionResult DeleteCompte(int id,string Mod)
        {
            Advice(db.Publications.Find(id));
            if (db.Users.Find(id).Advice >= 3)
            {
                db.Users.Find(id).Status = false;

            }
            else
            {
                db.Users.Find(id).Advice += 1;
                

            }
            db.SaveChanges();
            return RedirectToAction("Index","Moderateur", new {a=Mod,Img=""});
        }
        public ActionResult DeletePublication(int id,string Mod)
        {
            db.Comments.Find(id).Status = false;
            db.SaveChanges();
            return RedirectToAction("Index", new { a = Mod, Img = "" });
        }

        public ActionResult RenitialiseCompte(int id)
        {
            db.Users.Find(id).Status = false;
            string Password=string.Empty;
            User user=new User();
            try
            {
                 Password = RandomString(16);
            }
            catch (Exception)
            {
                int a = 5;
            }
            user.UserName=db.Users.Find(id).UserName;
            user.UserName = db.Users.Find(id).Email;
            user.Password = Password;
            db.Users.Add(user);
            db.SaveChanges();
            try
            {
                RAccount(user, Password);
            }
            catch (Exception ex)
            {
                int pop = 1;
            }
            return View();

        }
        public  ActionResult ValidateInscription(int id, string Mod)
        {
            db.Users.Find(id).Status=true;
            db.SaveChanges();
            User user = db.Users.Find(id);
            try
            {
                ActiveAccount(user);
            }catch(Exception ex)
            {
                int pop =1;
            }
            
            return RedirectToAction("Index", new { a = Mod, Img = "" });
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
