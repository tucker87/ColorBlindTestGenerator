using System;
using System.Web.Mvc;
using ColorBlindTestGenerator.Models;

namespace ColorBlindTestGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string line, string type)
        {
            var image = ImageCreation.CreateImage(line, (ImageCreation.TestType) Enum.Parse(typeof(ImageCreation.TestType), type, true));
            var model = new IndexViewModel(image);
            return View(model);
        }
    }

    
}