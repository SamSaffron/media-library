using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.ORM {
    public class TableDefinition {
        public TableDefinition(string tableName) {
            TableName = tableName;
            ColumnDefinitions = new List<ColumnDefinition>();
            Indexes = new List<IndexDefinition>();
        }

        public string TableName { get; set; }

        public List<ColumnDefinition> ColumnDefinitions { get; private set; }
        public List<IndexDefinition> Indexes { get; private set; }

        public TableDefinition AddColumn(string columnName, Type type) {
            return AddColumn(columnName, type, ColumnProperties.None);
        }

        public TableDefinition AddColumn(string columnName, Type type, ColumnProperties properties) {
            AddColumn(new ColumnDefinition(columnName, type, properties));
            return this;
        }

        public TableDefinition AddColumn(ColumnDefinition columnDefinition) {
            ColumnDefinitions.Add(columnDefinition);
            return this;
        }

        public TableDefinition AddIndex(params string[] columns) {
            var def = new IndexDefinition();
            def.AddRange(columns);
            Indexes.Add(def);
            return this;
        }

    }
}
