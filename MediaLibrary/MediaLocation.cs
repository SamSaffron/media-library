using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using MediaLibrary.Helpers;

namespace MediaLibrary {
    public class MediaLocation : IMediaLocation {

        public MediaLocation(string path, FolderMediaLocation parent) {
            this.Path = path;
            this.Parent = parent;
        }

        public string Path { get; set;}
        public IFolderMediaLocation Parent { get; set; }
    }
}
