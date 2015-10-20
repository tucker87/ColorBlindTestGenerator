using System.Drawing;

namespace ColorBlindTestGenerator.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(Bitmap image)
        {
            Image = image;
            ImageByteArray = (byte[])new ImageConverter().ConvertTo(image, typeof(byte[]));
        }

        public Bitmap Image { get; set; }
        public byte[] ImageByteArray { get; set; }
    }
}