using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace MediaLibrary {
    public class MediaLocation {

        #region Known video extensions 

        public static Dictionary<string, bool> videoExtensions = 
            new Dictionary<string, bool>() {
                {".vob", false},
                {".bup", false}, 
                {".ifo", false}, 
                {".rmvb",true},
                {".mov",true},
                {".avi",true},
                {".mpg",true},
                {".mpeg",true},
                {".wmv",true},
                {".mp4",true},
                {".mkv",true},
                {".divx",true},
                {".dvr-ms",true},
                {".ogm",true}
            };

        #endregion 

        public MediaLocation(string path) : this(path, Directory.Exists(path)) { 
        }

        internal MediaLocation(string path, bool isFolder) {
            Path = path;
            IsFolder = isFolder;
        }

        public bool IsFolder { get; set; }
        public string Path { get; set;}


        IList<MediaLocation> children; 
        public IList<MediaLocation> Children { 
            get {
                if (!IsFolder) return null;

                if (children == null) {
                    children = GetChildren();
                }
                return children;
            }  
        }

        private IList<MediaLocation> GetChildren() {
            var children = new List<MediaLocation>();
            foreach (var file in Directory.GetFiles(Path)) {
                children.Add(new MediaLocation(file, false));
            }
            foreach (var dir in Directory.GetDirectories(Path)) {
                children.Add(new MediaLocation(dir, true));
            }
            return children;
        }

        public MediaLocation Parent { get; set; }


        public bool IsVideo {
            get 
            {
                if (IsFolder) return false;

                string extension = System.IO.Path.GetExtension(Path).ToLower();
                
                bool isVideo; 
                if (videoExtensions.TryGetValue(extension, out isVideo))
                {
                    return isVideo;
                }

                string pt = null;
                RegistryKey key = Registry.ClassesRoot;
                key = key.OpenSubKey(extension);
                if (key != null) {
                    pt = key.GetValue("PerceivedType") as string;
                }
                if (pt == null) pt = "";
                pt = pt.ToLower();

                lock (videoExtensions) {
                    videoExtensions[extension] = (pt == "video");
                }

                return videoExtensions[extension];
                
            }
        }
    }
}
