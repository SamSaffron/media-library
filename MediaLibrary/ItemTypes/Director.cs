using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ItemTypes {
    class Director : Folder {
        public Director(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
