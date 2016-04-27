using news.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace news.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            using (NewsterContext context = new NewsterContext())
            {
                return View(context.Articles.ToList());
            }
        }
 

        public ActionResult Search()
        {
            NewsterContext context = new NewsterContext();

            string searchString = Request["searchString"];

            var articleList = context.Articles.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchResult = articleList.Where(n => (n.Heading.ToLower().Contains(searchString.ToLower())) ||
                (n.Text.ToLower().Contains(searchString.ToLower())) || (n.Categories.Where(y => y.Name.ToLower().Contains(searchString.ToLower())).Count() > 0) || (n.User.UserName.ToLower().Contains(searchString.ToLower())));

                List<Article> searchArticles = new List<Article>();

                foreach (var item in searchResult)
                {
                    searchArticles.Add(item);
                }
                
                if (searchArticles.Count()!=0)
                {
                    return View("Index", searchArticles);
                }
            }
            return RedirectToAction("Index");
            

        }
    }
}