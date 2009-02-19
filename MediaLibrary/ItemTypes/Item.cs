using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaLibrary.Helpers;

namespace MediaLibrary
{
    public abstract class Item
    {
        public Item(Library library, IMediaLocation mediaLocation, Item parent) {
            this.Library = library;
            this.Location = mediaLocation.Path;
            this.Parent = parent;
            this.Name = Path.GetFileNameWithoutExtension(mediaLocation.Path);
            this.id = mediaLocation.Path.GetMD5();
        }

        public virtual string Name {
            get;
            protected set;
        }

        public virtual string DateModified {
            get;
            protected set;
        }

        Guid id; 
        public virtual Guid Id {
            get {
                return id;
            }
            set {
                this.id = value;
            }
        }

        public string Location  {
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
