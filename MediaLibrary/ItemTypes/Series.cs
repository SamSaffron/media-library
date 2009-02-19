using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Series : Folder {
        public Series(Library library, FolderMediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
