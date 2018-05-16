using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Interaktionslogik für PlaylistDialog.xaml
    /// </summary>
    public partial class PlaylistDialog : Window
    {
        MediaLoader media;
        ObservableCollection<Media> files;
        public PlaylistDialog(Object media)
        {
            this.media = (MediaLoader)media;
            files = new ObservableCollection<Media>();
            DataContext = media;
            InitializeComponent();
            FileList.ItemsSource = files;
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
                    if (!files.Contains(m)) files.Add(m);
                }
        }

        private void CreatePlaylist(object sender, RoutedEventArgs e)
        {
            media.Sources.Add(PName.Text.TrimEnd().TrimStart(), files);
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
            files.Remove(i.Content as Media);
        }

        /*private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ResponseText)) DialogResult = true;
            else MessageBox.Show("Please enter something");
        }

        private void ResponseTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(ResponseText)) DialogResult = true;
            else MessageBox.Show("Please enter something");
        }*/
    }
}
