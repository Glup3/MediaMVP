using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaMVP
{
    class Media
    {
        private String mName, mPath, mFilename, mext;
        public String MName
        {
            get { return mName; }
            set
            {
                mName = value;
            }
        }
        public String MPath
        {
            get { return mPath; }
            set
            {
                mPath = value;
            }
        }
        public String MFilename
        {
            get { return mFilename; }
            set
            {
                mFilename = value;
            }
        }
        public String Mext
        {
            get { return mext; }
            set
            {
                mext = value;
            }
        }

        public Media(String path)
        {
            this.MPath = path;
            MName = Path.GetFileName(path);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Media)) return false;
            var item = obj as Media;

            return MPath.Equals(item.MPath);
        }
    }
}
