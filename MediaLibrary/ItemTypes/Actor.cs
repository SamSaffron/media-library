﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ItemTypes {
    public class Actor : Folder {
        public Actor(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }
    }
}
