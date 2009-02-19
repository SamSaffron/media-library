using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Episode : Video {
        public Episode(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
