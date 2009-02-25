using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.ORM.Attributes;

namespace MediaLibrary {
    public class Video : Item {

        [Column]
        public bool IsWatched { get; set; }
        [Column]
        public int PlayCount { get; set; }
        [Column]
        public DateTime LastPlayed { get; set; }
    }
}
