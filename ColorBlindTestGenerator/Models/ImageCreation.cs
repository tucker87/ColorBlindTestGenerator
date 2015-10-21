using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;

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
                g.DrawString(line, new Font("Tahoma", 26, FontStyle.Bold), new SolidBrush(color), x, y);
            }
        }

        public static void DrawPattern(this Bitmap image)
        {
            var rng = new Random();
            for (var y = 0; y < image.Height; y++)
            {
                Debug.WriteLine(y);
                var batch = new Batch(rng);
                for (var x = 0; x < image.Width; x++)
                {
                    if (batch.Size == 0)
                        batch = new Batch(rng);

                    image.SetPixel(x, y,
                        Colors[image.GetPixel(x, y).Name.ToColorGroup(), batch.Color]);

                    batch.Size--;
                }
            }
        }

        private class Batch
        {
            public Batch(Random rng)
            {
                Color = (ColorShade) rng.Next(1, 3);
                Size = rng.Next(1, 6);
            }

            public ColorShade Color { get; }
            public int Size { get; set; }
        }

        private static ColorGroup ToColorGroup(this string name)
        {
            return ColorGroups.ContainsKey(name)
                ? ColorGroups[name]
                : ColorGroup.Background;
        }

        public class MultiKeyDictionary<TKey1, TKey2, TValue> : Dictionary<TKey1, TValue>
        {
            private Dictionary<Tuple<TKey1, TKey2>, TValue> _innerDictionary;

            public MultiKeyDictionary()
            {
                _innerDictionary = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
            }

            public void Add(TKey1 key1, TKey2 key2, TValue value)
            {
                _innerDictionary.Add(new Tuple<TKey1, TKey2>(key1, key2), value);
            }

            private TValue GetValue(TKey1 key1, TKey2 key2)
            {
                return _innerDictionary[new Tuple<TKey1, TKey2>(key1, key2)];
            }

            private void SetValue(TKey1 key1, TKey2 key2, TValue value)
            {
                _innerDictionary[new Tuple<TKey1, TKey2>(key1, key2)] = value;
            }

            public TValue this[TKey1 key1, TKey2 key2]
            {
                get { return GetValue(key1, key2); }
                set { SetValue(key1, key2, value); }
            }
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