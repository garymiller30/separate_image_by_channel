using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreviewSeparateFile.models
{
    public class PreviewFile
    {
        public string FileName { get; set; }
        public Dictionary<string,string> SeparationsFile = new Dictionary<string,string>();
        public string Cyan => SeparationsFile["Cyan"];
        public string Magenta => SeparationsFile["Magenta"];
        public string Yellow => SeparationsFile["Yellow"];
        public string Black => SeparationsFile["Black"];

        public PreviewFile(string fileName)
        {
            FileName = fileName;
        }
        
    }
}
