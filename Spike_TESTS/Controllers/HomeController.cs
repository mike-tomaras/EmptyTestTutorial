using System.Collections.Generic;
using System.Web.Mvc;
using Spike_TESTS.Models;

namespace Spike_TESTS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return null;
        }

        [HttpPost]
        public ActionResult Save(HomeViewModel model)
        {
            return null;
        }
    }

    public class HomeViewModel
    {
        //For the list
        public IEnumerable<User> Users { get; set; }

        //For the form
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}