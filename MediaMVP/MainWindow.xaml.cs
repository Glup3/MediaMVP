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
using System.Windows.Controls.Primitives;
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
        private bool playing = true;
        private bool first;
        private bool wait;
        Animationen animationen;
        DispatcherTimer inactivity;
        DispatcherTimer player;
        MediaLoader media;
        public ExtensionDialog extdia;

        public MainWindow()
        {
            inactivity = new DispatcherTimer();
            animationen = new Animationen();
            player = new DispatcherTimer();
            player.Tick += new EventHandler(play_time);
            player.Interval = new TimeSpan(0, 0, 0, 0, 166);
            media = new MediaLoader();
            DataContext = media;
            InitializeComponent();
            player.Start();
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Thickness mag = new Thickness();
            if (e.Key == Key.F11 && !first)
            {
                first = true;
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
                Menu.Visibility = Visibility.Collapsed;
                Source.Hide();
                Manage.Hide();
                Filter.Hide();
                Media.Hide();
                mag.Left = 0;
                mag.Top = 0;
                mag.Right = 0;
                mag.Bottom = 0;
                dockingManager.Margin = mag;
                Mediaplayer.Title = "";
                Mediaplayer.CanAutoHide = false;
                Mediaplayer.CanClose = false;
                Mediaplayer.CanFloat = false;
                Mediaplayer.CanHide = false;
                TimelineSlider.Width = 500;
            }
            else if ((e.Key == Key.F11 || e.Key == Key.Escape) && first)
            {
                first = false;
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
                Menu.Visibility = Visibility.Visible;
                Source.Show();
                Manage.Show();
                Filter.Show();
                Media.Show();
                mag.Left = 5;
                mag.Top = 2;
                mag.Right = 5;
                mag.Bottom = 2;
                dockingManager.Margin = mag;
                Mediaplayer.Title = "Mediaplayer";
                Mediaplayer.CanAutoHide = true;
                Mediaplayer.CanClose = true;
                Mediaplayer.CanFloat = true;
                Mediaplayer.CanHide = true;
                TimelineSlider.Width = 100;
                Cursor = Cursors.Arrow;
                animationen.NormalModus(null, null, new List<Control> { PauseMedia, TimelineSlider, PlayTime }, null);
            }
        }

        /*
         * Counter, falls der User den Courser nicht mehr bewegt 
         */
        public void timerforfadeout()
        {
            inactivity.Tick += new EventHandler(inactivity_Tick);
            inactivity.Interval = new TimeSpan(0, 0, 0, 1, 500);
            inactivity.Start();

        }

        private void play_time(object sender, EventArgs e)
        {
            if (Player.HasAudio || Player.HasVideo)
            {
                String ts = Player.Position.ToString();
                int start = ts.LastIndexOf(".");
                if (start > -1) PlayTime.Content = ts.Remove(start, ts.Length - start);
                else PlayTime.Content = ts;
                if (playing) TimelineSlider.Value = Player.Position.TotalMilliseconds;
            }
            else PlayTime.Content = "";
        }
        private void inactivity_Tick(object sender, EventArgs e)
        {
            if (first)
            {
                animationen.fullscreenModus(null, null, new List<Control> { PauseMedia, TimelineSlider, PlayTime }, null);
                animationen.createStoryboard(PastMedia, "Height", 20, 0, 500, 0);
                animationen.createStoryboard(PauseMedia, "Height", 20, 0, 500, 0);
                animationen.createStoryboard(NextMedia, "Height", 20, 0, 500, 0);
                animationen.createStoryboard(TimelineSlider, "Height", 20, 0, 500, 0);
                animationen.createStoryboard(PlayTime, "Height", 20, 0, 500, 0);
            }
            // Hier kommt der Code für FadeOut hin, wenn die Maus in Fullscreen nicht mehr bewegt wird.
            Cursor = Cursors.None;
            wait = true;
            if (!first)
            {
                Cursor = Cursors.Arrow;
                wait = false;
            }
            inactivity.Stop();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (first)
            {
                Cursor = Cursors.Arrow;
                if (wait)
                {
                    animationen.NormalModus(null, null, new List<Control> { PauseMedia, TimelineSlider, PlayTime }, null);
                    animationen.createStoryboard(PastMedia, "Height", 0, 20, 500, 0);
                    animationen.createStoryboard(PauseMedia, "Height", 0, 20, 500, 0);
                    animationen.createStoryboard(NextMedia, "Height", 0, 20, 500, 0);
                    animationen.createStoryboard(TimelineSlider, "Height", 0, 20, 500, 0);
                    animationen.createStoryboard(PlayTime, "Height", 0, 20, 500, 0);

                    // Hier kommt der Code für FadeIn hin, wenn die Maus in Fullscreen bewegt wird.
                    wait = false;
                }
                timerforfadeout();
            }
            if (!first && wait)
            {
                animationen.createStoryboard(PauseMedia, "Opacity", 0, 1, 250, 0);
                animationen.createStoryboard(PauseMedia, "Opacity", 0, 1, 250, 0);
                animationen.createStoryboard(NextMedia, "Opacity", 0, 1, 250, 0);
                animationen.createStoryboard(TimelineSlider, "Opacity", 0, 1, 250, 0);
                animationen.createStoryboard(PlayTime, "Opacity", 0, 1, 250, 0);
                animationen.createStoryboard(PastMedia, "Height", 0, 20, 250, 0);
                animationen.createStoryboard(PauseMedia, "Height", 0, 20, 250, 0);
                animationen.createStoryboard(NextMedia, "Height", 0, 20, 250, 0);
                animationen.createStoryboard(TimelineSlider, "Height", 0, 20, 250, 0);
                animationen.createStoryboard(PlayTime, "Height", 0, 20, 250, 0);
                wait = false;
            }
        }

        private void OpenDirectoryDialog(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                String path = dialog.SelectedPath;
                if (result.ToString().Equals("OK"))
                {
                    int idx = Sources.SelectedIndex;

                    media.Sources.Remove("Path");
                    media.Sources["Path"] = MediaLoader.GetMediaENum(path, media.UsedExtensions);
                    if (idx != Sources.SelectedIndex)
                    {
                        Sources.SelectedIndex = media.Sources.Count - 1;
                        media.Media = media.Sources["Path"];
                        media.CMedia = null;
                    }
                    File.WriteAllText(media.path, path);
                }
            }
        }

        private void OpenExtDialog(object sender, RoutedEventArgs e)
        {
            if (extdia == null)
            {
                extdia = new ExtensionDialog(this, media);
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
            media.ReloadMedia(Sources.SelectedValue as ObservableCollection<Media>);
        }

        private void RemovePlaylist(object sender, RoutedEventArgs e)
        {
            var idx = Sources.SelectedIndex;
            var i = (KeyValuePair<String, ObservableCollection<Media>>)Sources.SelectedItem;
            media.Sources.Remove(i.Key);
            Sources.SelectedIndex = idx > 0 ? idx - 1 : 0;
        }

        private void EditPlaylist(object sender, RoutedEventArgs e)
        {
            EditPlaylist ep = new EditPlaylist(media, Sources.SelectedItem);
            ep.Owner = this;
            ep.Show();
        }
        void OnMouseDownPlayMedia(object sender, RoutedEventArgs args)
        {
            if (playing)
            {
                playing = false;
                Player.Pause();
            }
            else
            {
                playing = true;
                Player.Play();
            }
            if (TimelineSlider.Value == TimelineSlider.Maximum)
                Player.Position = new TimeSpan(0, 0, 0, 0, 0);
            // InitializePropertyValues();

        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            // Player.Volume = (double)volumeSlider.Value;
        }

        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            // Player.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            TimelineSlider.Maximum = Player.NaturalDuration.TimeSpan.TotalMilliseconds;
            TimelineSlider.Value = Player.Position.TotalMilliseconds;
            Player.Play();
        }

        private void Element_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            Medias.SelectedIndex += 1;
        }


        private void DragStarted(object sender, DragStartedEventArgs args)
        {
            Player.Pause();
            playing = false;
        }

        private void DragCompleted(object sender, DragCompletedEventArgs args)
        {
            playing = true;
            int SliderValue = (int)TimelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            Player.Position = ts;
            Player.Play();
        }

        void InitializePropertyValues()
        {
            //Player.Volume = (double)volumeSlider.Value;
            //Player.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Medias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player.Play();
            playing = true;
            TimelineSlider.Value = 0;
        }

        private void Sources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medias.SelectedIndex = 0;
            TimelineSlider.Value = 0;
            Player.Stop();
            playing = false;
        }

        private void TimelineSlider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("ss");
            int SliderValue = (int)TimelineSlider.Value;
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            Player.Position = ts;
        }
    }

}
