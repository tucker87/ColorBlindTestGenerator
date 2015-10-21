using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        
        public ActionResult GetImage(string greenText, string redText)
        {
            return new FileStreamResult(ImageCreation.CreateImage(greenText, redText).SaveToStream(), "image/png");
        }
    }

    //This should be in another file.
    public static class Extension
    {
        public static MemoryStream SaveToStream(this Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            ms.Position = 0;
            return ms;
        }
    }
}