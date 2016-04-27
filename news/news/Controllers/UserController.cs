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

        [HttpPost]
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

        [HttpPost]
        public ActionResult Login()
        {
            ///Get values from the form entered by the user in /Default/Login.cshtml
            string tmpUsername = Request["LoginInput"];
            string tmpPassword = HelpClass.Encrypt(Request["PasswordInput"]);
            
            ///Connect to db
            using(NewsterContext nc = new NewsterContext())
            {
                ///Find user
                var tmpUserList = nc.Users.Where(x => x.UserName == tmpUsername);

                ///Check if there is a user and the password matches
                if (tmpUserList.Count() == 1 && tmpUserList.First().Password == tmpPassword)
                {
                    ///Set session values
                    Session["currentUserId"] = tmpUserList.First().UserId;
                    Session["currentUsername"] = tmpUserList.First().UserName;
                    Session["loginStatus"] = true;
                }
            }

            ///Return to index
            return RedirectToAction("/Default/Index");
        }

        public ActionResult Logout()
        {
            ///reset session values
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["loginStatus"] = false;

            return RedirectToAction("/Default/Index");
        }
    }
}