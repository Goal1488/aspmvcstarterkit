using System.Linq;
using System.Web.Mvc;
using Entities.Users;
using FuckThisNumber.Interfaces.Repository;

namespace FuckThisNumber.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAsyncRepository _repository;

        public HomeController(IAsyncRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var q  = _repository.Entities<AppUser>().ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}