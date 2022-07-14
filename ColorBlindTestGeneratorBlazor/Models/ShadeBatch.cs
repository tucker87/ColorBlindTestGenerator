using System;
using static ColorBlindTestGeneratorBlazor.Models.ColorDataTypes;

namespace ColorBlindTestGeneratorBlazor.Models
{
    public class ShadeBatch
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
}