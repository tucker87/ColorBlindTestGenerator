using ColorBlindTestGeneratorBlazor.Models;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static ColorBlindTestGeneratorBlazor.Models.ColorDataTypes;

namespace ColorBlindTestGeneratorBlazor.Extensions
{
    public static class ImageExtensions
    {
        public static void DrawText(this Image<Rgba32> image, DrawingOptions options, string line, Font font, Color color, int x, int y)
        {
            var point = new PointF(x, y);

            image.Mutate(x => x.DrawText(options, line, font, color, point));
        }

        public static void DrawPattern(this Image<Rgba32> image)
        {
            var shadeBatch = new ShadeBatch();
            for (var y = 0; y < image.Height; y++)
                for (var x = 0; x < image.Width; x++)
                    image[x, y] = Colors[(image[x, y].ToColorGroup(), shadeBatch.Next())];
        }
    }
}
