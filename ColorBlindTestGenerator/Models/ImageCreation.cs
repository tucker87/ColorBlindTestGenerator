using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;

namespace ColorBlindTestGenerator.Models
{
    public static class ImageCreation
    {
        private static Dictionary<ColorType, Color> Colors => new Dictionary<ColorType, Color>
        {
            {ColorType.BackgroundLight, Color.FromArgb(149, 149, 149)},
            {ColorType.BackgroundDark, Color.FromArgb(114, 114, 114)},
            {ColorType.GreenDark, Color.FromArgb(165, 82, 117)},
            {ColorType.GreenLight, Color.FromArgb(215, 107, 152)},
            {ColorType.RedDark, Color.FromArgb(195, 97, 115)},
            {ColorType.RedLight, Color.FromArgb(254, 127, 150)},
            {ColorType.TextDark, Color.FromArgb(195, 136, 147)},
            {ColorType.TextLight, Color.FromArgb(215, 150, 177)}
        };

        public static Bitmap Image { get; private set; }
        
        public static Bitmap CreateImage(string line, TestType type)
        {
            Image = new Bitmap(392, 42);

            Image.DrawLine(line, type == TestType.Red ? Color.Red : Color.Green);

            Image.DrawBackground();
            
            return Image;
        }

        public static Bitmap DrawBackground(this Bitmap image)
        {
            var rng = new Random();
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var x = 0;
                    while (x < image.Width)
                    {
                        var batch = rng.Next(1, 6);
                        var isDark = rng.Next(1, 3) == 1;

                        while (batch > 0 && x < image.Width)
                        {
                            var currentPixel = image.GetPixel(x, y);
                            if (currentPixel.R > 0 || currentPixel.G > 0 || currentPixel.B > 0)
                                Console.WriteLine();

                            Color color;
                            switch (currentPixel.Name)
                            {
                                case "ffff0000":
                                    color = isDark
                                    ? Colors[ColorType.RedDark]
                                    : Colors[ColorType.RedLight];
                                    break;
                                case "ff008000":
                                    color = isDark
                                    ? Colors[ColorType.GreenDark]
                                    : Colors[ColorType.GreenLight];
                                    break;
                                default:
                                    color = isDark
                                ? Colors[ColorType.BackgroundDark]
                                : Colors[ColorType.BackgroundLight];
                                    break;
                            }
                            image.SetPixel(x, y, color);
                            x++;
                            batch--;
                        }
                    }
                }
            }

            return image;
        }

        public static Bitmap DrawLine(this Bitmap image, string line, Color color)
        {
            using (var g = Graphics.FromImage(image))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.DrawString(line, new Font("Tahoma", 22), new SolidBrush(color), 0, 0);
                return image;
            }
        }

        private enum ColorType
        {
            BackgroundLight,
            BackgroundDark,
            GreenDark,
            GreenLight,
            RedDark,
            RedLight,
            TextDark,
            TextLight
        }

        public enum TestType
        {
            Red,
            Green
        }
    }
}