using news.Models;
using System.Web.Mvc;

namespace news.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            using (NewsterContext nc = new NewsterContext())
            {
                
            }
                return View();
        }
    }
}