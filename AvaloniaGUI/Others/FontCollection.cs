using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Fonts;

namespace AvaloniaGUI.Others
{
    public static class FontSettings
    {
        public static Uri Key { get; } = new("fonts:", UriKind.Absolute);
        public static Uri Source { get; } = new("avares://AvaloniaGUI/Assets#OPlusSans 3.0", UriKind.Absolute);
    }

    /*public static class AlibabaFontSettings
    {
        public static Uri Key { get; } = new Uri("fonts:AlibabaPuHuiTi", UriKind.Absolute);
        public static Uri Source { get; } = new Uri("avares://WHOrigin.FrontDesk/Assets/Fonts/AliBaba", UriKind.Absolute);
    }*/

    public static class Extensions
    {
        public static AppBuilder UseFontFamily(this AppBuilder builder)
        {
            builder.With(new FontManagerOptions
            {
                DefaultFamilyName = "avares://AvaloniaGUI/Assets#OPlusSans 3.0",
                FontFallbacks = new[]
                {
                    new FontFallback
                    {
                        FontFamily = new FontFamily("avares://AvaloniaGUI/Assets#OPlusSans 3.0")
                    }
                }
            });
            return builder.ConfigureFonts(manager =>
                    manager.AddFontCollection(new EmbeddedFontCollection(FontSettings.Key, FontSettings.Source)))
                //.ConfigureFonts(manager => manager.AddFontCollection(new EmbeddedFontCollection(AlibabaFontSettings.Key, AlibabaFontSettings.Source)))
                ;
        }
    }
}