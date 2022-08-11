using PreviewSeparateFile.models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PreviewSeparateFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dir = string.Empty;
        ObservableCollection<PreviewFile<BitmapImage>> listfiles = new ObservableCollection<PreviewFile<BitmapImage>>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = ((PreviewFile<BitmapImage>)((ListBoxItem)sender).Content).FileName, UseShellExecute = true });
        }

        private void LoadPreview(ObservableCollection<PreviewFile<BitmapImage>> listfiles, string[] files)
        {
            foreach (var file in files)
            {
                var previewFile = new PreviewFile<BitmapImage>(file);
                var separator = new separator.Separator();
                previewFile.SeparationsFile = separator.ProcessFileToStream(file);

                App.Current.Dispatcher.Invoke(() =>
                {
                    listfiles.Add(previewFile);
                    progressBar.Value +=1;
                });
            }
        }

        async private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list.ItemsSource = listfiles;
            string[] files;

            if (Directory.Exists(dir))
            {
                files = Directory.GetFiles(dir, "*.pdf");
            }
            else if (File.Exists(dir))
            {
                files = new string[] { dir };
            }
            else
            {
                throw new Exception("File or Directory not exists");
            }

            progressBar.Maximum = files.Length;

            await Task.Run(() => LoadPreview(listfiles, files));
        }

        public void OpenFolderOrFile(string path)
        {
            dir = path;
        }
    }
}
