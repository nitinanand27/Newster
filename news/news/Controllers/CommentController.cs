using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddComment()
        {
            NewsterContext context = new NewsterContext();
            string comment = Request["comment"];
            int articleId = Convert.ToInt16(Request["articleCommented"]);
            int currentUserId = (int)Session["currentUserId"];
            var user = context.Users.Where(u => u.UserId == currentUserId).First();
            Article article = context.Articles.Where(a => a.ArticleId == articleId).FirstOrDefault();


            Comment newComment = new Comment
            {
                Date = DateTime.Now,
                ArticleId = articleId,
                User = user,
                Text = comment
            };

            context.Comments.Add(newComment);
            context.SaveChanges();

            return Redirect("/Default/Index");
        }
    }
}