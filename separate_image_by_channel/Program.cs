
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var separator = new separator.Separator();
            separator.ProcessFile(inputPdf);
            
        }
    }
}
