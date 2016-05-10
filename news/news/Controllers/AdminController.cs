using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// Present the dashboard view with a list of all the articles
        /// the current user has rights to edit and delete. For admin there
        /// is also a option to approve new users
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                using(NewsterContext context = new NewsterContext())
                {

                    ViewBag.Categories = context.Categories.ToList();

                    string tmpUser = Session["currentUserName"].ToString();

                    if(tmpUser == "admin")
                    {

                        ///Create list of unauthorised users for admin to see
                        ViewBag.ListOfNewUsers = context.Users.Where(x => x.Confirmed == false).ToList();

                        ///Admin can see all articles in the dB
                        return View(context.Articles.ToList());
                    }

                    ///Check for confirmed user
                    else if (context.Users.Where(x=>x.UserName == tmpUser).First().Confirmed == true)
                    {

                        ///Get all the articles created by the specific user
                        var articleList = context.Articles.Where(x => x.User.UserName == tmpUser).ToList();

                        return View(articleList);
                    }

                    ///Show "not approved"-view
                    else
                    {
                        return View("NotApproved");
                    }
                }
            }

            catch
            {
                return Redirect("/Default/Index");
            }
        }

        /// <summary>
        /// Create new article
        /// </summary>
        /// <returns></returns>
        public ActionResult AddArticle()
        {
            NewsterContext context = new NewsterContext();

            string heading = Request["heading"];
            string imageurl = Request["imageurl"];
            string videourl = Request["videourl"];
            string sourceurl = Request["sourceurl"];
            string text = Request["content"];
            string category = Request["categories"];
            string userName = (string)Session["currentUserName"];

            Category cat = context.Categories.Where(x => x.Name == category).First();
            User tmpUser = context.Users.Where(x => x.UserName == userName).First();

            ///Heading, source and text is mandatory. Text max 140 characters.
            if(heading.Length>0 && text.Length>0 && sourceurl.Length > 0 && text.Length<=140)
            {
                Article newArticle = new Article
                {
                    Heading = heading,
                    User = tmpUser,
                    Date = DateTime.Now,
                    ImgAdress = imageurl,
                    VideoAdress = videourl,
                    SourceAdress = sourceurl,
                    Text = text,
                    Category = cat
                };

                tmpUser.Articles.Add(newArticle);
                cat.Articles.Add(newArticle);

                context.Articles.Add(newArticle);
                context.SaveChanges();
                return Redirect("/Default/Index");
            }

            else
            {
                TempData["createError"] = "Heading, text and source is mandatory! Text can't be longer than 140 characters!";
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Delete article
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            using(NewsterContext nc = new NewsterContext())
            {
                ///Find article and delete it
                int idToDelete = int.Parse(Request["articleIdDelete"]);

                Article tmpArticle = nc.Articles.Where(x => x.ArticleId == idToDelete).First();
                nc.Articles.Remove(tmpArticle);
                nc.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Approve new user that have registred on the site
        /// </summary>
        /// <returns></returns>
        public ActionResult Approve()
        {

            ///Get id from view
            int idToApprove = int.Parse(Request["approveId"]);

            using(NewsterContext nc = new NewsterContext())
            {
                ///Set confirmed
                nc.Users.Where(x => x.UserId == idToApprove).First().Confirmed = true;
                nc.SaveChanges();
            }

            return RedirectToAction("index");
        }
    }
}