using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaMVP
{
    class Extension : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name;
        public string Name
        {
            get { return name; }
            set { name = value;  OnPropertyChanged(); }
        }
        public bool selected = true;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }

        public Extension(String name)
        {
            this.Name = name;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override bool Equals(object obj)
        {
            if (obj==null||!(obj is Extension)) return false;
            var item = obj as Extension;

            return Name.Equals(item.Name);
        }

        public override int GetHashCode()
        {
            var hashCode = -1565929788;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + selected.GetHashCode();
            hashCode = hashCode * -1521134295 + Selected.GetHashCode();
            return hashCode;
        }
    }
}
