using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;

namespace news.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult RegisterUser()
        {

            string tmpUsername = "";
            string tmpEmail = "";
            string tmpPassword = HelpClass.Encrypt("");

            ///Check entries
            if(tmpUsername.Length>2 && HelpClass.ValidateEmail(tmpEmail) && tmpPassword.Length > 6)
            {
                ///Connect to dB and create user
                using (NewsterContext nc = new NewsterContext())
                {

                    User tmpUser = new User() { UserName = tmpUsername, Email = tmpEmail, Password = tmpPassword };
                    nc.Users.Add(tmpUser);
                    nc.SaveChanges();
                }
            }

            return RedirectToAction("/Default/Index");
        }

        public ActionResult Login()
        {

            string tmpUsername = Request["LoinInput"];
            string tmpPassword = HelpClass.Encrypt(Request["PasswordInput"]);
            
            using(NewsterContext nc = new NewsterContext())
            {
                var tmpUserList = nc.Users.Where(x => x.UserName == tmpUsername);

                if (tmpUserList.Count() == 1 && tmpUserList.First().Password == tmpPassword)
                {
                    Session["currentUserId"] = tmpUserList.First().UserId;
                    Session["currentUsername"] = tmpUserList.First().UserName;
                    Session["loginStatus"] = true;
                }
            }

            return RedirectToAction("/Default/Index");
        }

        public ActionResult Logout()
        {

            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["loginStatus"] = false;

            return RedirectToAction("/Default/Index");
        }
    }
}