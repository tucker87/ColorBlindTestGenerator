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
        public ActionResult Index(string greenText, string redText)
        {
            var image = ImageCreation.CreateImage(greenText, redText);
            var model = new IndexViewModel(image);
            return View(model);
        }
    }

    
}