using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Helpers;

namespace MediaLibrary {
    public class VirtualFolderMediaLocation : MediaLocation, IFolderMediaLocation {

        VirtualFolder virtualFolder; 

        public VirtualFolderMediaLocation(string path, IFolderMediaLocation parent) 
            : base(path, parent) 
        {
            virtualFolder = new VirtualFolder(Contents);
            children = new Lazy<IList<IMediaLocation>>(GetChildren);
        }

        Lazy<IList<IMediaLocation>> children;

        private IList<IMediaLocation> GetChildren() {
            var children = new List<IMediaLocation>();
            foreach (var item in virtualFolder.Folders) {
                var location = new FolderMediaLocation(item, null, this);
                foreach (var child in location.Children) {
                    children.Add(child);
                }
            }
            return children;
        }


        public IList<IMediaLocation> Children {
            get { return children.Value; }
        }

    }
}
