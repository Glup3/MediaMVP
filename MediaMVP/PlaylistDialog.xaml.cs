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
        public PlaylistDialog(Object media)
        {
            this.media = (MediaLoader)media;
            InitializeComponent();
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            String f = "";
            foreach (Extension ext in media.UsedExtensions)
            {
                f += ext.Name+" (*.png)|"+"*"+ext.Name+"|";
            }
            Debug.WriteLine(f);
            openFileDialog.Filter = f;
            if (openFileDialog.ShowDialog().ToString() == "OK")
                System.Windows.MessageBox.Show(openFileDialog.FileName);
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
