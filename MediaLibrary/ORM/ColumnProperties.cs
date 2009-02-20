using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM {
    [Flags]
    public enum ColumnProperties {
        None = 0,
        PrimaryKey = 1,
        AutoIncrement = 2
    } 
}
