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

                if (context.Articles.Count() > 0)
                {
                    var commentList = context.Comments.Include("User").ToList();
                    var list = context.Articles.Include("User").Include("Category").Include("Comments").ToList();

                    return View(list);
                }
                return View("Empty");
            }
        }
 

        public ActionResult Search()
        {
            NewsterContext context = new NewsterContext();

            string searchString = Request["searchString"];

            var articleList = context.Articles.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                //checks if search text matches article heading, description or author name irrespective of case
                var searchResult = articleList.Where(n => (n.Heading.ToLower().Contains(searchString.ToLower())) ||
                (n.Text.ToLower().Contains(searchString.ToLower())) || (n.Category.Name.ToLower().Contains(searchString.ToLower()))
                || (n.User.UserName.ToLower().Contains(searchString.ToLower()))).ToList();

                //if match result is positive
                if (searchResult.Count() != 0)
                {
                    //returns a view with search result
                    return View("Index", searchResult);
                }

                //foreach (var item in searchResult)
                //{
                //    item.User = (from x in context.Users
                //                 where x.UserId == item.User.UserId
                //                 select x).First();
                //}

                
            }

            return RedirectToAction("Index");            

        }
    }
}