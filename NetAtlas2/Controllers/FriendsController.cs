using NetAtlas2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetAtlas2.Controllers
{
    public class FriendsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Friends
        public List<int> myfriend = new List<int>();
        public List<int> myInvitation = new List<int>();
        public ActionResult Index(int? a, string Img)
        {
            myInvitation.Clear();
            myfriend.Clear();
            int userId = Convert.ToInt32(Session["UseId"]);
            ViewBag.UserId = userId;
            ViewBag.username = db.Users.Find(userId).UserName;
            User user = db.Users.Find(userId);
            var users = db.Users.Where(x => x.Id==0).ToList();
            ViewBag.Invitation = db.Users.Where(x => x.Id == 0).ToList();
            ViewBag.Suggestions= db.Users.Where(x => x.Id == 0).ToList();
            ViewBag.test = user.AllFriend;
            ViewBag.Users = db.Users.Where(x => x.Status == true).ToList();
            string Invitation = user.Invitation;
            if (!string.IsNullOrEmpty(Invitation))
            {
                string[] vi = Invitation.Split();
                ViewBag.Id = user.Id;

                vi.ToList().ForEach(x =>
                {
                    ViewBag.Invitation.Add(db.Users.Find(int.Parse(x)));

                });
            }
            if (!string.IsNullOrEmpty(user.AllFriend))
            {
                string temp = user.AllFriend;
                string[] vs = temp.Split();
                vs.ToList().ForEach(x =>
                {
                    myfriend.Add(int.Parse(x));

                });
                if (myfriend.Count != 0)
                {
                    foreach (var friend in myfriend)
                    {
                        //ViewBag.Count=FriendsId.Count;
                        users.Add(db.Users.Find(friend));
                    }
                    foreach(var us in ViewBag.Users)
                    {
                        if(!ViewBag.Invitation.Contains(us) && !users.Contains(us))
                        {
                            ViewBag.Suggestions.Add(us);
                        }
                    }
                    myfriend.Clear();
                    ViewBag.Search = "Ah you have friend baka";
                    return View(users);
                }
            }

            ViewBag.Search = "No Friend you are just lonely";
            myfriend.Clear();
            return View();

            /*
             foreach (string v in vs)
             {
                 ViewBag.Count=v.GetType();
                // myfriend.Add(x);
             }
             /*var FriendsId = user.LocalFriend(temp);*/
            /*
            if (myfriend.Count != 0)
            {
                foreach (var friend in myfriend)
                {
                    //ViewBag.Count=FriendsId.Count;
                    users.Add(db.Users.Find(friend));
                }
                return View(users);
            }
            ViewBag.Search = "no";*/
           
        }
        // Post: Post search
        public ActionResult PostSearch(string Search)
        {   if (Search != null)
            {
                myInvitation.Clear();
                myfriend.Clear();
                string temp = Search;
                int userId = Convert.ToInt32(Session["UseId"]);
                var Users = db.Users.Where(x => x.UserName.Contains(temp)).ToList();
                User user = db.Users.Find(userId);
                //*********************$******************************//
                var users = db.Users.Where(x => x.Id == 0).ToList();
                ViewBag.Invitation = db.Users.Where(x => x.Id == 0).ToList();
                ViewBag.Suggestions = db.Users.Where(x => x.Id == 0).ToList();
                ViewBag.test = user.AllFriend;
                ViewBag.Users = db.Users.Where(x => x.Status == true).ToList();
                //********************$*******************************//
                string Allfriend = user.AllFriend;
                ViewBag.friend = Allfriend;
                ViewBag.username = db.Users.Find(userId);

                string Invitation = user.Invitation;
                ViewBag.Id = user.Id;
                ///*************************$*******************************///
                if (!string.IsNullOrEmpty(user.AllFriend))
                {
                    string ten = user.AllFriend;
                    string[] vs = ten.Split();
                    vs.ToList().ForEach(x =>
                    {
                        myfriend.Add(int.Parse(x));

                    });
                    if (myfriend.Count != 0)
                    {
                        foreach (var friend in myfriend)
                        {
                            //ViewBag.Count=FriendsId.Count;
                            users.Add(db.Users.Find(friend));
                        }
                        foreach (var us in ViewBag.Users)
                        {
                            if (!ViewBag.Invitation.Contains(us) && !users.Contains(us))
                            {
                                ViewBag.Suggestions.Add(us);
                            }
                        }
                        myfriend.Clear();
                        ViewBag.Search1 = "Ah you have friend baka";
                    }
                }
                ///*************************$*******************************///
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
                    ViewData["listfriend"] =new List<int>();
                }
                if(!string.IsNullOrEmpty(Invitation))
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
                }
                if (Users.Count > 0)
                {
                    ViewBag.Search = "yes";
                    return View(Users);
                }
                Search = null;
            }
            ViewBag.Search1 = "No Friend you are just lonely";
            ViewBag.Search = "No corresponding response to the search";
            return View();
        }
        // Post: Post search

        public ActionResult AddFriend(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(userId).AllFriend;
            string a = $"{id}";
            if (temp == null)
            {
                temp = $"{id}";
            }
            else if ((!string.IsNullOrEmpty(temp)) && (!temp.Contains(a)))
            {
                temp += $" {id}";
            }
            else
            {
                return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
            }
            //var user = db.Users.FirstOrDefault(x => x.Id == id);
            db.Users.Find(userId).AllFriend = temp;
            db.SaveChanges();
            temp = db.Users.Find(id).Invitation;
            a = $"{userId}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = $"{userId}";
            }
            else if ((!string.IsNullOrEmpty(temp)) && (!temp.Contains(a)))
            {
                temp += $" {userId}";
            }
            else
            {
                return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
            }
            db.Users.Find(id).Invitation = temp;
            db.SaveChanges();
            myfriend.Clear();
            return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
        }
        public ActionResult UnAddFriend(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(userId).AllFriend;
            string a = $"{id}";
            if (( temp.Contains(a) && temp.Length==a.Length)) 
            {
                temp = "";
            }
            else if ((temp.IndexOf(a)==1) && temp.Contains(a))
            {
                temp = temp.Replace(a, "");
            }
            else if((temp.IndexOf(a) != 1) && temp.Contains(a))
            {
                temp = temp.Replace(" " + a, "");
            }
            db.Users.Find(userId).AllFriend = temp;
            db.SaveChanges();
            myfriend.Clear();
            //*****************************************************suppression coté Invité********************************************************//
            temp = db.Users.Find(id).AllFriend;
            a = $"{userId}";
            if ((temp.IndexOf(a) == 1) && temp.Contains(a) && temp.Length == a.Length)
            {
                temp = "";
            }
            else if ((temp.IndexOf(a) == 1) && temp.Contains(a))
            {
                temp = temp.Replace(a, "");
            }
            else if ((temp.IndexOf(a) != 1) && temp.Contains(a))
            {
                temp = temp.Replace(" " + a, "");
            }
            db.Users.Find(id).AllFriend = temp;
            db.SaveChanges();
            myfriend.Clear();
            return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });

        }
        public ActionResult InviteFriend(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(id).Invitation;
            string a = $"{userId}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = $"{userId}";
            }
            else if ((!string.IsNullOrEmpty(temp)) && (!temp.Contains(a)))
            {
                temp += $" {userId}";
            }
            else
            {
                return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
            }
            db.Users.Find(id).Invitation = temp;
            db.SaveChanges();
            myfriend.Clear();
            return RedirectToAction("Index", "Friends");

        }
         public ActionResult IndexInvitation()
        {
            myInvitation.Clear();
            myfriend.Clear();
            int userId = Convert.ToInt32(Session["UseId"]);
            ViewBag.UserId = userId;
            User user = db.Users.Find(userId);
            var users = db.Users.Where(x => x.Id == 0).ToList();
            if ( !string.IsNullOrEmpty(user.Invitation))
            {
                string temp = user.Invitation;
                string[] vs = temp.Split();
                vs.ToList().ForEach(x =>
                {
                    myfriend.Add(int.Parse(x));

                });
                if (myfriend.Count != 0)
                {
                    foreach (var friend in myfriend)
                    {
                        //ViewBag.Count=FriendsId.Count;
                        users.Add(db.Users.Find(friend));
                    }
                    myfriend.Clear();
                    ViewBag.Search = "Ah you have friend baka";
                    return View(users);
                }
            }
            ViewBag.Search = "No Friend you are just lonely";
            myfriend.Clear();
            return View();

        }
        public ActionResult AgreeInvitation(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(userId).AllFriend;
            string a = $"{id}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = $"{id}";
            }
            else if ((!string.IsNullOrEmpty(temp)) && (!temp.Contains(a)))
            {
                temp += $" {id}";
            }
            else
            {
                return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
            }
            //var user = db.Users.FirstOrDefault(x => x.Id == id);
            db.Users.Find(userId).AllFriend = temp;
            db.SaveChanges();
            //*********************************************************************************************//
           


            //*********************************************************************************************//

            temp = db.Users.Find(userId).Invitation;
            a = $"{id}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = string.Empty;
            }
            if (( temp.Contains(a) && temp.Length==a.Length))
            {
                temp =  string.Empty;
            }
            else if ((temp.IndexOf(a) == 1) && temp.Contains(a) && temp.Length > a.Length)
            {
                temp = temp.Replace(a, string.Empty);
            }
            else if ((temp.IndexOf(a) != 1) && temp.Contains(a))
            {
                temp = temp.Replace(" "+ a, string.Empty);
            }
            db.Users.Find(userId).Invitation = temp;
            db.SaveChanges();
            //**************************************************************************************//
            temp = db.Users.Find(id).AllFriend;
            a = $"{userId}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = $"{userId}";
            }
            else if ((!string.IsNullOrEmpty(temp)) && (!temp.Contains(a)))
            {
                temp += $" {userId}";
            }
            else
            {
                return RedirectToAction("Index", "Friends", new { a = Session["UseId"].ToString(), Img = Session["ImageUrl"].ToString() });
            }
            //var user = db.Users.FirstOrDefault(x => x.Id == id);
            db.Users.Find(id).AllFriend = temp;
            db.SaveChanges();
            if (db.Users.Find(userId).AllFriend.Contains(db.Users.Find(userId).Invitation)&& db.Users.Find(userId).Invitation.Length==1)
            {
                db.Users.Find(userId).Invitation="";
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Friends");
            //*********************************************************************************************//
        }
        
        public ActionResult RefuseInvitation(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(userId).Invitation;
            string a = $"{id}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = string.Empty;
            }
            if ((temp.Contains(a) && temp.Length == a.Length))
            {
                temp = string.Empty;
            }
            else if ((temp.IndexOf(a) == 1) && temp.Contains(a) && temp.Length > a.Length)
            {
                temp = temp.Replace(a, string.Empty);
            }
            else if ((temp.IndexOf(a) != 1) && temp.Contains(a))
            {
                temp = temp.Replace(" " + a, string.Empty);
            }
            db.Users.Find(userId).Invitation = temp;
            db.SaveChanges();
            return RedirectToAction("Index", "Friends");
        }
        public ActionResult Abort(int id)
        {
            int userId = Convert.ToInt32(Session["UseId"]);
            string temp = db.Users.Find(id).Invitation;
            string a = $"{userId}";
            if (string.IsNullOrEmpty(temp))
            {
                temp = string.Empty;
            }
            if ((temp.Contains(a) && temp.Length == a.Length))
            {
                temp = string.Empty;
            }
            else if ((temp.IndexOf(a) == 1) && temp.Contains(a) && temp.Length > a.Length)
            {
                temp = temp.Replace(a, string.Empty);
            }
            else if ((temp.IndexOf(a) != 1) && temp.Contains(a))
            {
                temp = temp.Replace(" " + a, string.Empty);
            }
            db.Users.Find(id).Invitation = temp;
            db.SaveChanges();
            return RedirectToAction("Index", "Friends");
        }
    }
}
