using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM.Attributes {
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class IndexAttribute : Attribute {

        readonly string[] columns;

        // This is a positional argument
        public IndexAttribute(params string[] columns) {
            this.columns = columns;
        }

        public string[] Columns {
            get { return columns; }
        }
    }
}
