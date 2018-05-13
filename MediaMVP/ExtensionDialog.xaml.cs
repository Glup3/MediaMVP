using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MediaMVP
{
    /// <summary>
    /// Interaktionslogik für ExtensionDialog.xaml
    /// </summary>
    public partial class ExtensionDialog : Window
    {
        MainWindow main;
        MediaLoader media;
        public ExtensionDialog(MainWindow main, object media)
        {
            this.main = main;
            this.media = (MediaLoader)media;
            DataContext = media;
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            int idx = main.Sources.SelectedIndex;
            if (idx >= 0) media.ReloadMedia(media.Sources.ElementAt(idx).Value);
            else media.LoadExtensions();
            main.extdia = null;
            base.OnClosing(e);
        }

        private void AddExtension(object sender, RoutedEventArgs e)
        {
            var dialog = new StringDialog();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                Extension ext = new Extension(dialog.ResponseText);
                if (!media.FreeExtensions.Contains(ext)&&!media.UsedExtensions.Contains(ext)) media.FreeExtensions.Add(ext);
                else MessageBox.Show("Extension already exists!");
            }
        }

        private void AddToUsed(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var con = item.Content as Extension;
            media.UsedExtensions.Add(con);
            media.FreeExtensions.Remove(con);
            con.Selected = true;
        }

        private void AddToFree(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TextBlock;
            var con = new Extension(item.Text as string);
            media.FreeExtensions.Add(con);
            media.UsedExtensions.Remove(con);
            /*var item = sender as ListViewItem;
            var con = item.Content as Extension;
            media.FreeExtensions.Add(con);
            media.UsedExtensions.Remove(con);
            Event="PreviewMouseLeftButtonDown" Handler="AddToFree"*/
        }

        private void RemoveFree(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var con = item.Content as Extension;
            media.FreeExtensions.Remove(con);
        }

        private void RemoveUsed(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            var con = item.Content as Extension;
            media.UsedExtensions.Remove(con);
        }
    }
}
