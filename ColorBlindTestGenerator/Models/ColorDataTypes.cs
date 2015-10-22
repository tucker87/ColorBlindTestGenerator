using System;
using System.Collections.Generic;
using System.Drawing;
using TuckersToolbox;

namespace ColorBlindTestGenerator.Models
{
    public static class ColorDataTypes
    {
        public static MultiKeyDictionary<ColorGroup, ColorShade, Color> Colors => new MultiKeyDictionary<ColorGroup, ColorShade, Color>
        {
            {ColorGroup.Background, ColorShade.Dark, Color.FromArgb(114, 114, 114)},
            {ColorGroup.Background, ColorShade.Light, Color.FromArgb(149, 149, 149)},
            {ColorGroup.Red, ColorShade.Dark, Color.FromArgb(195, 97, 115)},
            {ColorGroup.Red, ColorShade.Light, Color.FromArgb(254, 127, 150)},
            {ColorGroup.Green, ColorShade.Dark, Color.FromArgb(165, 82, 117)},
            {ColorGroup.Green, ColorShade.Light, Color.FromArgb(215, 107, 152)},
            {ColorGroup.Blue, ColorShade.Dark, Color.FromArgb(126,104,177)},
            {ColorGroup.Blue, ColorShade.Light, Color.FromArgb(171,155,200)},
            {ColorGroup.Text, ColorShade.Dark, Color.FromArgb(195, 136, 147)},
            {ColorGroup.Text, ColorShade.Light, Color.FromArgb(215, 150, 177)}
        };
        private static Dictionary<string, ColorGroup> ColorGroups => new Dictionary<string, ColorGroup>
        {
            {"ffff0000", ColorGroup.Red},
            {"ff008000", ColorGroup.Green},
            {"ff0000ff", ColorGroup.Blue},
            {"ff000000", ColorGroup.Text}
        };

        public static ColorGroup ToColorGroup(this string name)
        {
            return ColorGroups.ContainsKey(name)
                ? ColorGroups[name]
                : ColorGroup.Background;
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