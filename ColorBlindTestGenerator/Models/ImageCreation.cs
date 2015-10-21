using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using TuckersToolbox;

namespace ColorBlindTestGenerator.Models
{
    public static class ImageCreation
    {
        private static MultiKeyDictionary<ColorGroup, ColorShade, Color> Colors => new MultiKeyDictionary<ColorGroup, ColorShade, Color>
        {
            {ColorGroup.Background, ColorShade.Dark, Color.FromArgb(114, 114, 114)},
            {ColorGroup.Background, ColorShade.Light, Color.FromArgb(149, 149, 149)},
            {ColorGroup.Green, ColorShade.Dark, Color.FromArgb(165, 82, 117)},
            {ColorGroup.Green, ColorShade.Light, Color.FromArgb(215, 107, 152)},
            {ColorGroup.Red, ColorShade.Dark, Color.FromArgb(195, 97, 115)},
            {ColorGroup.Red, ColorShade.Light, Color.FromArgb(254, 127, 150)},
            {ColorGroup.Text, ColorShade.Dark, Color.FromArgb(195, 136, 147)},
            {ColorGroup.Text, ColorShade.Light, Color.FromArgb(215, 150, 177)}
        };

        private static Dictionary<string, ColorGroup> ColorGroups => new Dictionary<string, ColorGroup>
        {
            {"ffff0000", ColorGroup.Red},
            {"ff008000", ColorGroup.Green}
        };

        public static Bitmap Image { get; private set; }
        
        public static Bitmap CreateImage(string greenText, string redText)
        {
            Image = new Bitmap(392, 100);
            Image.DrawText(greenText, Color.Green, 0, 0);
            Image.DrawText(redText, Color.Red, 0, 50);
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

        private class ShadeBatch
        {
            public ShadeBatch()
            {
                Random = new Random();
                NewBatch();
            }

            private void NewBatch()
            {
                Color = (ColorShade)Random.Next(1, 3);
                Size = Random.Next(1, 6);
            }

            public ColorShade Next()
            {
                if (Size-- == 0)
                    NewBatch();

                return Color;
            }

            private ColorShade Color { get; set; }
            private int Size { get; set; }
            private Random Random { get; }
        }

        private static ColorGroup ToColorGroup(this string name)
        {
            return ColorGroups.ContainsKey(name)
                ? ColorGroups[name]
                : ColorGroup.Background;
        }

        private enum ColorGroup
        {
            Background,
            Green,
            Red,
            Text
        }

        private enum ColorShade
        {
            Dark = 1,
            Light = 2
        }

        public enum TestType
        {
            Red,
            Green
        }
    }
}