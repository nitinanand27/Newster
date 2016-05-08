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
        /// the current user has rights to edit and delete.
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                using(NewsterContext context = new NewsterContext())
                {

                    ViewBag.Categories = context.Categories.ToList();

                    string tmpUser = Session["currentUserName"].ToString();

                    if (tmpUser != "admin")
                    {

                        ///Get all the articles created by the specific user
                        var articleList = context.Articles.Where(x => x.User.UserName == tmpUser).ToList();

                        return View(articleList);

                    }

                    ///Admin can see all articles in the dB
                    return View(context.Articles.ToList());
                }
            }

            catch
            {
                return Redirect("/Default/Index");
            }
        }

        public ActionResult Create()
        {
            using (NewsterContext nc = new NewsterContext())
            {
                ViewBag.Categories = nc.Categories.ToList();
            }
            return View();
        }

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

            var existingHeadings = context.Articles.Where(h => h.Heading == heading);

            //Controls adding new article with same heading
            if (existingHeadings.Count() == 0)
            {
                Category cat = context.Categories.Where(x => x.Name == category).First();
                User tmpUser = context.Users.Where(x => x.UserName == userName).First();

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

                cat.Articles.Add(newArticle);

                context.Articles.Add(newArticle);
                context.SaveChanges();
                return Redirect("/Default/Index");
            }

            else
            {
                TempData["headingError"] = "Heading already exists";
                return Redirect("/Admin/Create");
            }
        }

        public ActionResult Delete()
        {
            using(NewsterContext nc = new NewsterContext())
            {
                int idToDelete = int.Parse(Request["articleIdDelete"]);

                Article tmpArticle = nc.Articles.Where(x => x.ArticleId == idToDelete).First();
                nc.Articles.Remove(tmpArticle);
                nc.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}