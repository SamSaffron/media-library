using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Movie : Item {
        public Movie(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
