using PreviewSeparateFile.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PreviewSeparateFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dir = string.Empty;
        ObservableCollection<PreviewFile> listfiles = new ObservableCollection<PreviewFile>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = ((PreviewFile)((ListBoxItem)sender).Content).FileName, UseShellExecute = true });
        }

        private void LoadPreview(ObservableCollection<PreviewFile> listfiles, string[] files)
        {
            foreach (var file in files)
            {
                var previewFile = new PreviewFile(file);
                var separator = new separator.Separator();
                previewFile.SeparationsFile = separator.ProcessFile(file);

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            list.ItemsSource = null;
            
            foreach (var file in listfiles)
            {
                try
                {
                    File.Delete(file.Cyan);
                    File.Delete(file.Magenta);
                    File.Delete(file.Yellow);
                    File.Delete(file.Black);
                }
                catch (Exception ee)
                {
                    throw new Exception(ee.Message);
                }
            }


        }
    }
}
