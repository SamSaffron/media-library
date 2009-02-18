using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Episode : Item {
        public Episode(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
