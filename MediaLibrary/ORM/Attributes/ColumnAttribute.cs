using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM.Attributes {
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    sealed class ColumnAttribute : Attribute {

        // This is a positional argument
        public ColumnAttribute() {
        }
    }
}
