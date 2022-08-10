using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace separate_image_by_channel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputPdf = "06.tif.pdf";
           
            
            var outputImg = Rasterize(inputPdf);

            Separate(outputImg);

            
        }

        static string Rasterize(string input)
        {
            var output = input + ".jpg";

            var settings = new MagickReadSettings
            {
                Density = new Density(150, 150),
                ColorSpace = ColorSpace.CMYK,
                
            };
            var images = new MagickImageCollection();
            images.Read(input, settings);
            images[0].Format = MagickFormat.Jpg; //or .Tiff
            images[0].Write(output); // or ".tiff"

            return output;
        }

        static void Separate(string input)
        {
            List<string> colors = new List<string> { "C", "M", "Y", "K" };
            using (MagickImageCollection images = new MagickImageCollection())
            {
                images.Read(input);
                int n = 0;
                foreach (var channel in images[0].Separate(Channels.All))
                {
                    channel.Negate();
                    channel.Write(input + "_" + colors[n] + ".jpg");
                    n++;
                }
            }
        }
    }
}
