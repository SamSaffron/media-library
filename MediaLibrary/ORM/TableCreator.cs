using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace MediaLibrary.ORM {
    class TableCreator {

        SQLiteConnection connection;

        public TableCreator(SQLiteConnection connection) {
            this.connection = connection;
        }

        public void Create(TableDefinition definition) {

            StringBuilder builder = new StringBuilder("CREATE TABLE ");
            builder.Append(definition.TableName);
            builder.Append("(");
            bool first = true; 
            foreach (var column in definition.ColumnDefinitions) {
                if (!first) {
                    builder.Append(", ");
                } else {
                    first = false;
                }

                builder.Append(column.Name);

                if (column.IsPrimaryKey) {
                    builder.Append(" PRIMARY KEY");
                }
            }
            builder.Append(")");

            using (var cmd = connection.CreateCommand()) 
            {
                cmd.CommandText = builder.ToString();
                cmd.ExecuteNonQuery();
            }

        }
    }
}
