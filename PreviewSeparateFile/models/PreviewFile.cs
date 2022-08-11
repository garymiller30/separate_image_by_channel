using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreviewSeparateFile.models
{
    public class PreviewFile<T>
    {
        public string FileName { get; set; }
        public Dictionary<string,T> SeparationsFile = new Dictionary<string,T>();
        public T Cyan => SeparationsFile["Cyan"];
        public T Magenta => SeparationsFile["Magenta"];
        public T Yellow => SeparationsFile["Yellow"];
        public T Black => SeparationsFile["Black"];

        public PreviewFile(string fileName)
        {
            FileName = fileName;
        }
        
    }
}
