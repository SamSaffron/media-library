using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    public abstract class Item
    {
        public Item(Library library, MediaLocation mediaLocation, Item parent) {
            this.Library = library;
            this.MediaLocation = mediaLocation;
            this.Parent = parent;
        }

        public MediaLocation MediaLocation  {
            get;
            protected set;
        }

        public Library Library {
            get;
            protected set;
        }
        
        public Metadata Metadata {
            get {
                throw new NotImplementedException();
            }
        }

        public Item Parent {
            get;
            internal set;
        }

    }
}
