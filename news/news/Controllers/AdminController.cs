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
        // GET: Admin
        public ActionResult Index()
        {
            try
            {
                using(NewsterContext context = new NewsterContext())
                {

                    if(Session["currentUserName"].ToString() != "admin")
                    {
                        var articleList = context.Articles.Where(x => x.User.UserName == Session["currentUserName"].ToString());
                        return View(articleList);
                    }

                    return View("Create");
                }
            }

            catch
            {
                return Redirect("/Default/Index");
            }
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
    }
}