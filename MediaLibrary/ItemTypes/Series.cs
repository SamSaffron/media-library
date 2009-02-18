using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Series : Folder {
        public Series(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
