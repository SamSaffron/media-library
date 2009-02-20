using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM {
    public class ColumnDefinition {
        public ColumnDefinition(string columnName, Type type) :
            this(columnName, type, ColumnProperties.None) {
        }

        public ColumnDefinition(string columnName, Type type, ColumnProperties columnProperties) {
            this.Name = columnName;
            this.Type = type;
            this.ColumnProperties = columnProperties;
        }


        public string Name { get; set; }
        public Type Type { get; set; }
        public ColumnProperties ColumnProperties { get; set; }

        public bool IsPrimaryKey {
            get {
                return ((ColumnProperties & ColumnProperties.PrimaryKey) == ColumnProperties.PrimaryKey);
            }
        }

        public bool IsAutoIncrement {
            get {
                return ((ColumnProperties & ColumnProperties.AutoIncrement) == ColumnProperties.AutoIncrement);
            }
        }
    }
}
