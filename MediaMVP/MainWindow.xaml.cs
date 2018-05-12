using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            inactivity = new DispatcherTimer();
            InitializeComponent();
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, KeyEventArgs e)
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
    }
}
