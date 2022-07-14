using ColorBlindTestGeneratorBlazor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using ColorBlindTestGeneratorBlazor.Extensions;

namespace ColorBlindTestGenerator.Models
{
    public static class ImageCreation
    {
        private const int _lineHeight = 50;
        private static FontCollection _collection = new();
        private static HttpClient _http;
        private static Image<Rgba32> _image = new(100, 100);
        private static Font? _font = null;

        public static async Task<Image> CreateImage(List<TextDataModel> textData, HttpClient http)
        {
            _http = http;

            _image = new Image<Rgba32>(550, textData.Count * _lineHeight);

            await DrawText(textData);
            
            _image.DrawPattern();

            return _image;
        }

        private static async Task DrawText(List<TextDataModel> textData)
        {
            var currentLineHeight = 0;

            var font = await GetFont();

            foreach (var textItem in textData)
            {
                var color = Color.Parse(textItem.Color);
                _image.DrawText(DrawingOptions, textItem.Text, font, color, 10, currentLineHeight);
                currentLineHeight += _lineHeight;
            }
        }

        private static async Task<Font> GetFont()
        {
            if (_font == null)
            {
                if (!_collection.TryGet("Tahoma", out var family))
                {
                    var fontFileStream = await _http.GetStreamAsync("TAHOMABD.ttf");
                    _collection.Add(fontFileStream);
                    family = _collection.Get("Tahoma");
                }

                var font = family.CreateFont(40, FontStyle.Bold);

                _font = font;
            }

            return _font;
        }
        public static DrawingOptions DrawingOptions = new()
        {
            GraphicsOptions = new GraphicsOptions { Antialias = true }
        };
    }
}