using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ColorBlindTestGenerator.Models;
using Newtonsoft.Json;

namespace ColorBlindTestGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetImage(string data)
        {
            var textData = JsonConvert.DeserializeObject<List<TextDataModel>>(data);
            if (textData.Count == 0)
                textData = new List<TextDataModel>
                {
                    new TextDataModel {Color = "Black", Text = "No options selected!"},
                    new TextDataModel {Color = "Black", Text = "Are you trying to break it?"}
                };

            return new FileStreamResult(ImageCreation.CreateImage(textData).SaveToStream(), "image/png");
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