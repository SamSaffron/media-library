using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;

namespace MediaLibrary.ORM {
    
    // TODO: consider extracting a base interface so we can support multiple DBs
    // For now its sqlite only
    
    public class ItemRepository {

        static Schema schema = LoadSchema();
        const string ItemTypeTableName = "ItemType";

        private static Schema LoadSchema() {
            var schema = Schema.DiscoverSchema();
            schema.Add(new TableDefinition(ItemTypeTableName).
                AddColumn("Id", typeof(Guid), ColumnProperties.PrimaryKey).
                AddColumn("Type", typeof(string))
                );
            return schema;
        } 


        SQLiteConnection connection;

        public ItemRepository(string filename) {
            connection = new SQLiteConnection("Data Source=" + filename);
            connection.Open();
        }

        public void MigrateSchema() { 
            // Deal with upgrade later ... 
            var creator = new TableCreator(connection);

            using (var transaction = connection.BeginTransaction()) {
                foreach (var tableDef in schema) {
                    creator.Create(tableDef);
                }
                transaction.Commit();
            }
        }

        public T GetItem<T>(Guid id) where T : Item
        {
            throw new NotImplementedException();
        }

        public void SaveItem(Item item) {

            Debug.Assert(item != null);

            using (var tran = connection.BeginTransaction()) {

                foreach (var tableDef in GetTables(item.GetType())) {
                    var command = GetCommand(tableDef);
                    foreach (SQLiteParameter param in command.Parameters) {
                        if (param.ParameterName == "@Id" || param.ParameterName == "@ItemId") 
                        {
                            param.Value = item.Id;
                        } 
                        else if (param.ParameterName == "@Type" && tableDef.TableName == ItemTypeTableName)
                        {
                            param.Value = item.GetType().Name; 
                        } 
                        else 
                        {
                            string propertyName = param.ParameterName.Substring(1);
                            param.Value = item.GetType().GetProperty(propertyName).GetValue(item, null); 
                        }
                    }
                    command.ExecuteNonQuery();
                }

                tran.Commit();
            }
        }

        public void SaveItem(Item item, bool saveChildren) {
        }


        private SQLiteCommand GetCommand(TableDefinition def) {

            // Cache this ? - deal with conflict ... 

            var command = connection.CreateCommand();

            StringBuilder insertInto = new StringBuilder();
            StringBuilder values = new StringBuilder();

            insertInto.Append("INSERT INTO ").
                Append(def.TableName).
                Append("(");

            values.Append("VALUES(");

            bool first = true;
            foreach (var column in def.ColumnDefinitions) {

                if (!first) {
                    insertInto.Append(",");
                    values.Append(",");
                } 
                else {
                    first = false;
                }

                var param = command.CreateParameter();
                param.ParameterName = "@" + column.Name;
                command.Parameters.Add(param);

                insertInto.Append(column.Name);
                values.Append(param.ParameterName);
            }

            insertInto.Append(")");
            values.Append(")"); 

            command.CommandText = insertInto.ToString() + values.ToString();

            return command;
        }

        private List<TableDefinition> GetTables(Type type) {

            // TODO : Cache this 

            var tables = new List<TableDefinition>();
            foreach (var tableDef in schema) {

                if (tableDef.TableName == ItemTypeTableName) {
                    tables.Add(tableDef);
                    continue;
                }

                var baseType = type; 
                while(baseType != null) 
                {
                    if (tableDef.TableName == baseType.Name) {
                        tables.Add(tableDef);
                    }
                    baseType = baseType.BaseType;
                }
            }
            return tables;
        }
    }
}
