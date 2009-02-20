using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaLibrary.Helpers;
using MediaLibrary.ORM.Attributes;

namespace MediaLibrary
{
    [Index("Parent")]
    public abstract class Item
    {
        Guid id;

        public Item(Library library, IMediaLocation mediaLocation, Item parent) {
            this.Library = library;
            this.Location = mediaLocation.Path;
            this.Parent = parent;
            this.Name = Path.GetFileNameWithoutExtension(mediaLocation.Path);
            this.id = mediaLocation.Id;
        }

        public virtual bool IsValid() {
            return true;
        }

        [PrimaryKey]
        public virtual Guid Id {
            get { return id; }
            set { this.id = value; }
        }

        [Column]
        public virtual string Name {
            get;
            protected set;
        }

        [Column]
        public virtual string DateModified {  
            get; 
            protected set; 
        }

        [Column]
        public string Location  {
            get;
            protected set;
        }

        [Column]
        public Item Parent {
            get;
            internal set;
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

       

    }
}
