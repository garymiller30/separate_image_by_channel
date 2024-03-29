﻿using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace separator
{
    public class Separator
    {
        private List<string> _colors = new List<string> { "Cyan", "Magenta", "Yellow", "Black" };
        MagickReadSettings _settings = new MagickReadSettings
        {
            Density = new Density(150, 150),
            ColorSpace = ColorSpace.CMYK,

        };
        public Dictionary<string, string> ProcessFile(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File {path} not found");

            var images = new MagickImageCollection();
            try
            {
                images.Read(path, _settings);

                var channels = images[0].Separate(Channels.All);

                if (channels.Count != 4) throw new Exception("Channels are not equals 4");

                var dict = new Dictionary<string, string>
                {
                    { _colors[0], string.Empty },
                    { _colors[1], string.Empty },
                    { _colors[2], string.Empty },
                    { _colors[3], string.Empty }
                };

                int n = 0;
                foreach (var channel in channels)
                {
                    channel.Negate();
                    var separateFile = $"{path}_{_colors[n]}.jpg";
                    channel.Write(separateFile);
                    dict[_colors[n]] =separateFile;
                    n++;
                }
                return dict;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string, BitmapImage> ProcessFileToStream(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException($"File {path} not found");

            var images = new MagickImageCollection();
            try
            {
                images.Read(path, _settings);

                var channels = images[0].Separate(Channels.All);

                if (channels.Count != 4) throw new Exception("Channels are not equals 4");

                var dict = new Dictionary<string, BitmapImage>
                {
                    { _colors[0], new BitmapImage() },
                    { _colors[1], new BitmapImage()},
                    { _colors[2], new BitmapImage() },
                    { _colors[3], new BitmapImage()}
                };

                int n = 0;
                foreach (var channel in channels)
                {
                    channel.Negate();
                    
                    using(var stream = new MemoryStream())
                    {
                        channel.Write(stream,MagickFormat.Jpg);
                        stream.Position = 0;
                        var image = dict[_colors[n]];
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = stream;
                        image.EndInit();
                        image.Freeze();
                    }
                    
                    n++;
                }
                return dict;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
