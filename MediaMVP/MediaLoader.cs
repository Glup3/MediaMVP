using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaMVP
{
    class MediaLoader : INotifyPropertyChanged
    {
        public MediaLoader()
        {
            //GetMedia("");
           // Media = GetMediaENum("C:/Users/Granit/Desktop/KONFIG", new List<string> { ".mp3",".mp4"});
            sources = new Dictionary<string,IEnumerable<Media>>();
            sources.Add("Path", GetMediaENum("C:/Users/Granit/Desktop/KONFIG", new List<string> { ".mp3", ".mp4" }));
            sources.Add("PlayList 1", GetMediaENum("C:/Users/Granit/Desktop/KONFIG", new List<string> { ".mp4" }));
        }

        private Dictionary<string,IEnumerable<Media>> sources;
        public Dictionary<string,IEnumerable<Media>> Sources
        {
            get { return sources; }
            set { sources = value; OnPropertyChanged(); }
        }

        private IEnumerable<Media> media;
        public IEnumerable<Media> Media
        {
            get { return media; }
            set { media = value; OnPropertyChanged(); }
        }

        private Media cmedia;
        public Media CMedia
        {
            get { return cmedia; }
            set { cmedia = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public IEnumerable<Media> GetMediaENum(String path, List<String> ext)
        {
            /*var myFiles = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                 .Where(s => ext.Contains(Path.GetExtension(s)));
            */List<Media> res = new List<Media>(){ };
            foreach (String fileExtension in ext)
            {
                foreach (String file in Directory.EnumerateFiles(path,"*.*", SearchOption.AllDirectories))
                {
                    if(fileExtension.Contains(Path.GetExtension(file)))res.Add(new Media(file));
                }
            }
            return res;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
