using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NetAtlas2.Models;
using NetAtlas.ViewModels;
using System.Data.Entity;
using System.IO;
using NetAtlas2.ViewModels;

namespace NetAtlas2.Controllers
{
    public class ChatRoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET :ChatRoom
        private int USEId;
        private string IMG;
        public List<int> myfriend = new List<int>();
        public List<int> myInvitation = new List<int>();
        public ActionResult Index(int? ab, string Img)
        {
            int a = Convert.ToInt32(Session["UseId"]);
            var publications =db.Publications.Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList(); ;
            int userId = a;

            USEId = a;
            IMG = Img;
            ViewBag.UserId = a;
            ViewBag.Img = Img;
            var stranger = db.Users.Where(x => x.Status == true && !x.AllFriend.Contains(a + ""));
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            var photos =db.Photos.Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList(); ;
            var videos = db.Videos.Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList(); ;
            var liens = db.Liens.Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList(); ;
            var comments = db.Comments.Include(x => x.Replies).Where(x => x.Status == true && x.User.Status == true).OrderByDescending(x => x.CreatedOn).ToList(); ;
            ViewBag.comments = comments;
            ViewBag.videos=videos;
            ViewBag.liens=liens;
            ViewBag.photos=photos;
            ViewBag.stranger=stranger;
            ViewBag.ImageUrl = db.Users.Find(a).ImageUrl;
            ViewBag.username=db.Users.Find(a).UserName;
            //ViewBag.Allfriends = db.Users.Find(a).AllFriend.Split();
            ViewBag.Allfriends= db.Users.Find(a).AllFriend.Split().ToList().Count();
            //***********************************************************************************************************************************************************************************//
             myInvitation.Clear();
            myfriend.Clear();
            userId = Convert.ToInt32(Session["UseId"]);
            User user = db.Users.Find(userId);
            string Allfriend = user.AllFriend;
            ViewBag.friend = Allfriend;
            string Invitation = user.Invitation;
            ViewBag.Id = user.Id;
            ViewBag.Search = "yes";
            if (!string.IsNullOrEmpty(Allfriend))
            {
                string[] vs = Allfriend.Split();
                vs.ToList().ForEach(x =>
                {
                    myfriend.Add(int.Parse(x));

                });
                ViewData["listfriend"] = myfriend;
            }
            else
            {
                ViewData["listfriend"] = new List<int>();
            }
            if (!string.IsNullOrEmpty(Invitation))
            {
                string[] vi = Invitation.Split();
                ViewBag.Id = user.Id;

                vi.ToList().ForEach(x =>
                {
                    myInvitation.Add(int.Parse(x));

                });

                ViewData["listinvitation"] = myInvitation;
            }
            else
            {
                ViewData["listinvitation"] = new List<int>();
                ViewBag.Search = "No corresponding response to the search";
            }
            ViewBag.Search = "yes";


            //***********************************************************************************************************************************************************************************//    
            return View(publications);
        }
        //Post :ChatRoom/PostReply
        [HttpPost]
        public ActionResult PostReply(ReplyVM obj)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            Reply reply = new Reply();
            reply.Text = obj.Reply;
            reply.CommentId = obj.CID;
            reply.UserId = userId;
            reply.CreatedOn = DateTime.Now;

            db.Replys.Add(reply);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Post :ChatRoom/PostReply
        [HttpPost]
        public ActionResult PostComment(TextPostVM obj)
        {
            
            int userId = Convert.ToInt32(Session["UseId"]);
            ViewBag.UserId = userId;
            string type = obj.type;
            string titre = obj.titre;
            string commentText = obj.text;


            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (type == "Comment")
            {
                Comment comment = new Comment();
                comment.Text = commentText;
                comment.CreatedOn = DateTime.Now;
                comment.UserId = userId;
                comment.Status = true;
                comment.titre = titre;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            else if (type == "Lien")
            {
                Lien comment = new Lien();
                comment.Link = commentText;
                comment.CreatedOn = DateTime.Now;
                comment.UserId = userId;
                comment.Status = true;
                comment.titre = titre;
                db.Liens.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Index",new { a = Convert.ToInt32(Session["UseId"]) });
        }
        [HttpPost]
        public ActionResult PostPict(RessourceVM obj)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            userId = Convert.ToInt32(Session["UseId"]);
            string type = obj.type;
            string titre =obj.titre;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            if (type == "Photo")
            {
                var file = obj.Picture;
                Photo pic = new Photo();
                pic.UserId = userId;
                pic.CreatedOn = DateTime.Now;
                pic.Status = true;
                pic.titre = titre;

                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    string id_and_extension = userId + extension;
                    string imgUrl = "~/Videos and Images/" + id_and_extension;
                    pic.Phot_Picture = imgUrl;
                    //db.Entry(user).State = EntityState.Modified;
                    db.Photos.Add(pic);
                    db.SaveChanges();
                    var path = Server.MapPath("~/Videos and Images/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if ((System.IO.File.Exists(path + id_and_extension)))
                    {
                        System.IO.File.Delete(path + id_and_extension);
                    }
                    file.SaveAs(path + id_and_extension);
                }

            }
            else if (type == "Video")
            {
                var file = obj.Picture;
                Video pic = new Video();
                pic.UserId = userId;
                pic.CreatedOn = DateTime.Now;
                pic.Status = true;
                pic.titre = titre;

                if (file != null)
                {
                    var extension = Path.GetExtension(file.FileName);
                    string id_and_extension = userId + extension;
                    string imgUrl = "~/Videos and Images/" + id_and_extension;
                    pic.Vid_Picture= imgUrl;
                    //db.Entry(user).State = EntityState.Modified;
                    db.Videos.Add(pic);
                    db.SaveChanges();
                    var path = Server.MapPath("~/Videos and Images/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if ((System.IO.File.Exists(path + id_and_extension)))
                    {
                        System.IO.File.Delete(path + id_and_extension);
                    }
                    file.SaveAs(path + id_and_extension);
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult UserProfile()
        {
             return RedirectToAction("Index","User", new { a = Convert.ToInt32(Session["UseId"]), Img = Session["ImageUrl"].ToString() });
        }
        public ActionResult FriendsPanel()
        {
            return RedirectToAction("Index", "Friends", new { a = Convert.ToInt32(Session["UseId"]), Img = Session["ImageUrl"].ToString() });
        }
        public ActionResult DeletePublication(int id )
        {
            db.Publications.Find(id).Status = false;
            db.SaveChanges();
            return RedirectToAction("Index", new { a = Convert.ToInt64(Session["UseId"]), Img = "" });
        }
    }


}