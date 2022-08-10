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
           
            
            Separate(inputPdf);
       
            
        }

        static void Separate(string input)
        {
            List<string> colors = new List<string> { "C", "M", "Y", "K" };
            var settings = new MagickReadSettings
            {
                Density = new Density(150, 150),
                ColorSpace = ColorSpace.CMYK,
                
            };
            var images = new MagickImageCollection();
            images.Read(input, settings);

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
