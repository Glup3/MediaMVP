using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaMVP
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool first;
        private bool wait;
        DispatcherTimer inactivity;
        MediaLoader media;
        public ExtensionDialog extdia;

        public MainWindow()
        {
            inactivity = new DispatcherTimer();
            media = new MediaLoader();
            DataContext = media;
            InitializeComponent();
            var c = Sources.Items as ItemCollection;
            foreach (Extension li in c)
            {
                //Debug.WriteLine(li.Selected);
                //li.Selected = !li.Selected;
            }
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11 && !first)
            {
                first = true;
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                Cursor = Cursors.None;
            }
            else if (e.Key == Key.F11 && first)
            {
                first = false;
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
                Cursor = Cursors.Arrow;
            }
        }

        /*
         * Counter, falls der User den Courser nicht mehr bewegt 
         */
        public void timerforfadeout()
        {
            inactivity.Tick += new EventHandler(inactivity_Tick);
            inactivity.Interval = new TimeSpan(0, 0, 0, 3, 500);
            inactivity.Start();

        }

        private void inactivity_Tick(object sender, EventArgs e)
        {
            // Hier kommt der Code für FadeOut hin, wenn die Maus in Fullscreen nicht mehr bewegt wird.
            Cursor = Cursors.None;
            wait = true;
            inactivity.Stop();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (first)
            {
                Cursor = Cursors.Arrow;
                if (wait)
                {
                    // Hier kommt der Code für FadeIn hin, wenn die Maus in Fullscreen bewegt wird.
                    wait = false;
                }
                timerforfadeout();
            }

        }

        private void OpenDirectoryDialog(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
              System.Windows.Forms.DialogResult result = dialog.ShowDialog();
              String path = dialog.SelectedPath;
                //!String.IsNullOrEmpty(path)
                if (result.ToString().Equals("OK"))
                {
                    int idx = Sources.SelectedIndex;
                    
                    media.Sources.Remove("Path");
                    media.Sources["Path"] = MediaLoader.GetMediaENum(path,media.UsedExtensions);
                    if (idx!= Sources.SelectedIndex)
                    {
                       // Debug.WriteLine("a");
                        Sources.SelectedIndex = media.Sources.Count-1;
                        media.Media = media.Sources["Path"];
                        media.CMedia = null;
                    }
                    File.WriteAllText(media.path,path);
                }
            }
        }


            private void OpenExtDialog(object sender, RoutedEventArgs e)
        {
            if (extdia == null)
            {
                extdia = new ExtensionDialog(this,media);
                //extdia.Owner = this;
                extdia.Show();
            }
            else
            {
                extdia.Focus();
            }
        }

        private void AddPlaylist(object sender, RoutedEventArgs e)
        {
            PlaylistDialog pld = new PlaylistDialog(media);
            pld.Owner = this;
            pld.Show();
        }

        private void ActivateExt(object sender, MouseButtonEventArgs e)
        {
            var s = sender as ListViewItem;
            var c = s.Content as Extension;
            c.Selected = !c.Selected;
            Debug.WriteLine(c.Name+" "+c.Selected);
            media.ReloadMedia(Sources.SelectedValue as ObservableCollection<Media>);
            //c.Selected = !c.Selected;
        }

        private void RefreshExt(object sender, MouseWheelEventArgs e)
        {
            var lv = sender as ListView;
            var c = lv.Items as ItemCollection;
            foreach (Extension li in c)
            {
                //Debug.WriteLine(li.Selected);
               // li.Selected = !li.Selected;
            }
        }

        private void RemovePlaylist(object sender, RoutedEventArgs e)
        {
            var idx = Sources.SelectedIndex;
            var i = (KeyValuePair<String, ObservableCollection<Media>>)Sources.SelectedItem;
            media.Sources.Remove(i.Key);
            Sources.SelectedIndex = idx>0 ? idx-1:0;
        }

        private void EditPlaylist(object sender, RoutedEventArgs e)
        {
            EditPlaylist ep = new EditPlaylist(media,Sources.SelectedItem);
            ep.Owner = this;
            ep.Show();
        }
    }
}
