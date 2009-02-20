using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM.Attributes {
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PrimaryKeyAttribute : Attribute {
       
        public PrimaryKeyAttribute() {
        }

    }
   
}
