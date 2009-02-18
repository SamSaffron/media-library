using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MediaLibrary
{
    public abstract class Item
    {
        public Item(Library library, MediaLocation mediaLocation, Item parent) {
            this.Library = library;
            this.MediaLocation = mediaLocation;
            this.Parent = parent;
            this.Name = Path.GetFileNameWithoutExtension(mediaLocation.Path);
        }


        public virtual string Name {
            get;
            protected set;
        }

        public virtual string Id {
            get;
            protected set;
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
