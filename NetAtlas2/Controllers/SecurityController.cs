using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetAtlas2.Controllers
{
    public class SecurityController : Controller
    {
        //GET: SECURITY
        public ActionResult Logon()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
