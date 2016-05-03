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
                var list = context.Articles.ToList();

                foreach (var item in list)
                {

                    item.User = (from x in context.Users
                                 where x.UserId == item.Author
                                 select x).First();
                }

                return View(list);
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

                foreach (var item in searchResult)
                {
                    item.User = (from x in context.Users
                                 where x.UserId == item.Author
                                 select x).First();
                }
                
                if (searchResult.Count()!=0)
                {
                    return View("Index", searchResult);
                }
            }

            return RedirectToAction("Index");            

        }
    }
}