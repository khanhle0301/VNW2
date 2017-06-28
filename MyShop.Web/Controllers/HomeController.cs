using System.Web.Mvc;

namespace MyShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Footer()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Menu()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult Notification()
        {
            return PartialView();
        }
    }
}