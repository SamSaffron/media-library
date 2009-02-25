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
            set;
        }

        [Column]
        public virtual string DateModified {  
            get; 
            set; 
        }

        [Column]
        public string Uri  {
            get;
            set;
        }

        [Column]
        public Item Parent {
            get;
            set;
        }

        public Library Library {
            get;
            set;
        }
        
        public Metadata Metadata {
            get {
                throw new NotImplementedException();
            }
        }

       

    }
}
