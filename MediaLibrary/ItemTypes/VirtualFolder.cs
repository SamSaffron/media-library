using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class VirtualFolder : Folder {
        public VirtualFolder(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
