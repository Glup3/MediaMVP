using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MediaMVP
{
    class Animationen
    {
        private Storyboard story;
        private DoubleAnimation doublani;
        private DispatcherTimer inactivity;
        /*  
        * Erstellt eine Animation
        * createStoryboard(Name, "Path", from, to, dauer in milisekunden, verspätung in milisekunden);
        * zB:
        * createStoryboard(lautstärke, "Opacity", 0, 1, 5000, 500);
        */
        public void createStoryboard(Control resource, string propertytype, double fromvalue, double tovalue, int durationmili, int delaymili)
        {
            story = new System.Windows.Media.Animation.Storyboard();
            doublani = new DoubleAnimation();
            Storyboard.SetTarget(doublani, resource);
            Storyboard.SetTargetProperty(doublani, new PropertyPath(propertytype));
            doublani.From = fromvalue;
            doublani.To = tovalue;
            doublani.Duration = new TimeSpan(0, 0, 0, 0, durationmili);
            doublani.BeginTime = new TimeSpan(0, 0, 0, 0, delaymili);
            story.Children.Add(doublani);
            story.Begin();
        }

        public void fullscreenModus(List<Control> top, List<Control> right, List<Control> bottom, List<Control> left)
        {
            foreach (Control c in top)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 1, 0, 1000, 0);
            }
            foreach (Control c in right)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 1, 0, 1000, 0);
            }
            foreach (Control c in bottom)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 1, 0, 1000, 0);
            }
            foreach (Control c in left)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 1, 0, 1000, 0);
            }
        }
        public void NormalModus(List<Control> top, List<Control> right, List<Control> bottom, List<Control> left)
        {
            foreach (Control c in top)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 0, 1, 1000, 0);
            }
            foreach (Control c in right)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 0, 1, 1000, 0);
            }
            foreach (Control c in bottom)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 0, 1, 1000, 0);
            }
            foreach (Control c in left)
            {
                //createStoryboard(c, "Height", 50, 0, 1000, 0);
                //createStoryboard(c, "(Canvas.Top)", 150, 200, 1000, 0);
                createStoryboard(c, "Opacity", 0, 1, 1000, 0);
            }
        }

    }
}
