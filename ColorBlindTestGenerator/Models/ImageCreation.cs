using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using static ColorBlindTestGenerator.Models.ColorDataTypes;

namespace ColorBlindTestGenerator.Models
{
    public static class ImageCreation
    {
        public static Bitmap CreateImage(List<TextDataModel> textData)
        {
            const int lineHeight = 50;
            var currentLineHeight = 0;

            Image = new Bitmap(550, textData.Count * lineHeight);
            foreach (var textItem in textData)
            {
                Image.DrawText(textItem.Text, Color.FromName(textItem.Color), 0, currentLineHeight);
                currentLineHeight += lineHeight;
            }
            Image.DrawPattern();
            return Image;
        }

        private static void DrawText(this Image image, string line, Color color, int x, int y)
        {
            using (var g = Graphics.FromImage(image))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.DrawString(line, new Font("Tahoma", 30, FontStyle.Bold), new SolidBrush(color), x, y);
            }
        }

        public static void DrawPattern(this Bitmap image)
        {
            var shadeBatch = new ShadeBatch();
            for (var y = 0; y < image.Height; y++)
                for (var x = 0; x < image.Width; x++)
                    image.SetPixel(x, y,
                        Colors[image.GetPixel(x, y).Name.ToColorGroup(), shadeBatch.Next()]);
        }

        public static Bitmap Image { get; private set; }
    }
}