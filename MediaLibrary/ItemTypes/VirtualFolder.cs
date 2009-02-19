using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class VirtualFolder : Folder {
        public VirtualFolder(Library library, FolderMediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
