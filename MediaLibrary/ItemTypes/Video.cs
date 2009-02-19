using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public class Video : Item {
        public Video(Library library, IMediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }

        public bool IsWatched { get; set; }
        public int PlayCount { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
