using System.Collections.Generic;
using System.Web.Mvc;
using Spike_TESTS.Models;

namespace Spike_TESTS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }

    public class HomeViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}