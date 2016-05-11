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

            ///Get values from the form entered by the user in /Default/Login.cshtml
            string tmpUsername = Request["registerUsername"];
            string tmpEmail = Request["registerEmail"];
            string tmpPassword = HelpClass.Encrypt(Request["registerPassword"]);

            if(!HelpClass.ValidateEmail(tmpEmail) || tmpUsername.Length <= 1 || 
                tmpUsername.Length > 15 || tmpPassword.Length<6)
            {

                ///Return to login/register -view to try again
                TempData["error"] = "Please check username, email and password!";
                return Redirect("/Default/Login");
            }

            try
            {
                ///Connect to dB and create user
                using (NewsterContext nc = new NewsterContext())
                {
                    ///Check for existing username
                    if(nc.Users.Where(x=>x.UserName.ToLower() == tmpUsername.ToLower()).Count() == 0)
                    {
                        User tmpUser = new User() { UserName = tmpUsername, Email = tmpEmail, Password = tmpPassword,Confirmed=false };
                        nc.Users.Add(tmpUser);
                        nc.SaveChanges();
                    }

                    else
                    {
                        TempData["error"] = "User exists!";
                        return Redirect("/Default/Login");
                    }
                }

                return Redirect("/Default/Index");
            }

            catch
            {
                ///Return to login/register -view to try again
                TempData["error"] = "Database fail!";
                return Redirect("/Default/Login");
            }
        }

        [HttpPost]
        public ActionResult Login()
        {
            ///Get values from the form entered by the user in /Default/Login.cshtml
            string tmpUsername = Request["loginUsername"];
            string tmpPassword = HelpClass.Encrypt(Request["loginPassword"]);

            try
            {
                ///Connect to dB to find user
                using (NewsterContext nc = new NewsterContext())
                {
                    ///Find user
                    var tmpUserList = nc.Users.Where(x => x.UserName.ToLower() == tmpUsername.ToLower());

                    ///Check if there is a user and the password matches
                    if (tmpUserList.Count() == 1 && tmpUserList.First().Password == tmpPassword)
                    {
                        ///Set session values
                        Session["currentUserId"] = tmpUserList.First().UserId;
                        Session["currentUsername"] = tmpUserList.First().UserName;
                        Session["loginStatus"] = true;

                        return Redirect("/Default/Index");
                    }

                    else
                    {
                        TempData["error"] = "Check username and/or password";
                        return Redirect("/Default/Login");
                    }          
                }
            }

            catch
            {
                ///Return to login/register -view to try again
                TempData["error"] = "Database fail!";
                return Redirect("/Default/Login");
            }
        }

        public ActionResult Logout()
        {
            ///reset session values
            Session["currentUserId"] = "";
            Session["currentUsername"] = "";
            Session["loginStatus"] = false;

            return Redirect("/Default/Index");
        }
    }
}