using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaMVP
{
    /// <summary>
    /// Interaktionslogik für EditPlaylist.xaml
    /// </summary>
    public partial class EditPlaylist : Window
    {
        MediaLoader media;
        KeyValuePair<String, ObservableCollection<Media>> files;
        public EditPlaylist(object media, object files)
        {
            this.media = (MediaLoader)media;
            this.files = (KeyValuePair<String, ObservableCollection<Media>>)files;
            this.DataContext = media;
            InitializeComponent();
            FileList.ItemsSource = this.files.Value;
            PName.Text = this.files.Key;
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = MediaLoader.extString;
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog().ToString() == "OK")
                foreach (String file in openFileDialog.FileNames)
                {
                    Media m = new Media(file);
                   // if (!files.Contains(m)) files.Add(m);
                }
        }

        private void CreatePlaylist(object sender, RoutedEventArgs e)
        {
           // media.Sources.Add(PName.Text, files);
            var o = (MainWindow)Owner;
            o.Sources.SelectedIndex = o.Sources.Items.Count - 1;
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Owner.Focus();
        }

        private void RemoveMedia(object sender, MouseButtonEventArgs e)
        {
            var i = sender as System.Windows.Controls.ListViewItem;
            files.Value.Remove(i.Content as Media);
        }
    }
}
