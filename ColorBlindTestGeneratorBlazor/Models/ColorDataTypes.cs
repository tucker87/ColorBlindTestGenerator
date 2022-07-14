using SixLabors.ImageSharp.PixelFormats;

namespace ColorBlindTestGeneratorBlazor.Models
{
    public static class ColorDataTypes
    {
        public static Dictionary<(ColorGroup, ColorShade), Rgba32> Colors => new()
        {
            {(ColorGroup.Background, ColorShade.Dark), new Rgba32(114, 114, 114)},
            {(ColorGroup.Background, ColorShade.Light), new Rgba32(149, 149, 149)},
            {(ColorGroup.Red, ColorShade.Dark), new Rgba32(195, 97, 115)},
            {(ColorGroup.Red, ColorShade.Light), new Rgba32(254, 127, 150)},
            {(ColorGroup.Green, ColorShade.Dark), new Rgba32(165, 82, 117)},
            {(ColorGroup.Green, ColorShade.Light), new Rgba32(215, 107, 152)},
            {(ColorGroup.Blue, ColorShade.Dark), new Rgba32(126,104,177)},
            {(ColorGroup.Blue, ColorShade.Light), new Rgba32(171,155,200)},
            {(ColorGroup.Text, ColorShade.Dark), new Rgba32(195, 136, 147)},
            {(ColorGroup.Text, ColorShade.Light), new Rgba32(215, 150, 177)}
        };

        public static ColorGroup ToColorGroup(this Rgba32 color)
        {
            if (color.A != 0 && color.R == 0 && color.G == 0 && color.B == 0)
                return ColorGroup.Text;

            if (color.R > 0)
                return ColorGroup.Red;

            if (color.G > 0)
                return ColorGroup.Green;

            if (color.B > 0)
                return ColorGroup.Blue;

            return ColorGroup.Background;
        }

        public enum ColorGroup
        {
            Background,
            Green,
            Red,
            Text,
            Blue
        }

        public enum ColorShade
        {
            Dark = 1,
            Light = 2
        }
    }
}