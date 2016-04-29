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
            NewsterContext context = new NewsterContext();
            var articleList = context.Articles.ToList();
            return View(articleList);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult AddArticle()
        {
            NewsterContext context = new NewsterContext();

            string heading = Request["heading"];
            ////DateTime date = DateTime.Parse(Request["date"]);
            //int author = context.Users.Where(u => u.UserName == Session["currentUser"].ToString()).First().UserId;
            string imageurl = Request["imageurl"];
            string videourl = Request["videosurl"];
            string sourceurl = Request["sourceurl"];
            string text = Request["content"];
            //string categories = 

            //int authorID = Convert.ToInt16(Session["currentUserId"]);

            //var authorObject = context.Users.Where(u => u.UserId == authorID).FirstOrDefault();

            //int author = Convert.ToInt16(authorObject);

            var existingHeadings = context.Articles.Where(h => h.Heading == heading);

            //Controls adding new article with same heading
            if (existingHeadings.Count() == 0)
            {
                Article newArticle = new Article
                {
                    Heading = heading,
                    Author = 2,
                    Date = DateTime.Now,
                    ImgAdress = imageurl,
                    VideoAdress = videourl,
                    SourceAdress = sourceurl,
                    Text = text,
                    //Categories = new Category { Name = Request["category"]};
                };

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