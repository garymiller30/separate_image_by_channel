using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PreviewSeparateFile.models
{
    public class PreviewFile<T> : INotifyPropertyChanged
    {
        Dictionary<string, T> _separationsFile = new Dictionary<string, T>();


        public string FileName { get; set; }
        public Dictionary<string,T> SeparationsFile
        {
            get {return _separationsFile; }
            set { _separationsFile = value; OnPropertyChanged();}
        } 

        public event PropertyChangedEventHandler? PropertyChanged;

        public T Cyan => SeparationsFile["Cyan"];
        public T Magenta => SeparationsFile["Magenta"];
        public T Yellow => SeparationsFile["Yellow"];
        public T Black => SeparationsFile["Black"];

        public PreviewFile(string fileName)
        {
            FileName = fileName;
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
