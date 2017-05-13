using MVC5Course.Models;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();
        protected ProductRepository repo = RepositoryHelper.GetProductRepository();

        public ActionResult Debug()
        {
            return Content("Hello");
        }
    }
}