using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MediaLibrary {
    public class MediaLocation {

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
    }
}
