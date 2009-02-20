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
            id = new Lazy<Guid>(Path.GetMD5); 
        }

        public string Path { get; private set; }
        public IFolderMediaLocation Parent { get; private set; }

        Lazy<Guid> id; 
        public Guid Id {
            get { return id.Value; }
        }


    }
}
