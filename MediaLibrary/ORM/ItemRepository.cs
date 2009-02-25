using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using MediaLibrary.ORM.Attributes;

namespace MediaLibrary.ORM {
    
    // TODO: consider extracting a base interface so we can support multiple DBs
    // For now its sqlite only
    
    public class ItemRepository {

        static Schema schema = LoadSchema();
        static TableDefinition itemTypeTable = new TableDefinition(ItemTypeTableName).
                AddColumn("Id", typeof(Guid), ColumnProperties.PrimaryKey).
                AddColumn("Type", typeof(string)); 

        const string ItemTypeTableName = "ItemType";

        private static Schema LoadSchema() {
            var schema = Schema.DiscoverSchema();
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
                creator.Create(itemTypeTable);
                transaction.Commit();
            }
        }

        public IList<Item> GetItems(Guid parentId)
        {

            List<Item> items = new List<Item>();

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ItemType.Type, * FROM Item").
                AppendLine("JOIN ItemType on Item.Id = ItemType.Id");
            foreach (var table in schema) {
                if (table.TableName == "Item") continue;

                sql.Append("JOIN ").
                    Append(table.TableName).
                    Append(" ON Item.Id = ").
                    Append(table.TableName).
                    AppendLine(".ItemId");
            }

            sql.AppendLine("WHERE Item.Parent = @Guid");

            using (var command = CreateCommand(sql.ToString())) {
                var param = command.CreateParameter();
                param.ParameterName = "@Guid";
                param.Value = parentId;
                command.Parameters.Add(param);
                using (var reader = command.ExecuteReader())
                while (reader.Read()) {

                    Item instance = ConstructDefaultInstance(schema.MappedTypes[reader.GetString(0)]) as Item;
                    PopulateInstance(reader, instance);
                    items.Add(instance);
                }

                return items;
            }
        }

        public T GetItem<T>(Guid id) where T : Item
        {
            Type type = typeof(T);
            T instance = ConstructDefaultInstance(type) as T;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Item\n");
            foreach (var table in GetTables(typeof(T))) {
                if (table.TableName == "Item") continue;
  
                sql.Append("JOIN ").
                    Append(table.TableName).
                    Append(" ON Item.Id = ").
                    Append(table.TableName).
                    Append(".ItemId\n");  
            }

            using (var command = CreateCommand(sql.ToString()))
            using (var reader = command.ExecuteReader()) 
            while (reader.Read())
            {
                PopulateInstance(reader, instance);
                break; 
            }

            return instance;
        }

        private static object ConstructDefaultInstance(Type type) {
            return type.GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        private static void PopulateInstance (SQLiteDataReader reader, Item instance) 
        {
            // this should be refactored. 
            foreach (var property in instance.GetType().GetProperties()) {
                if (Schema.HasAttribute(property, typeof(ColumnAttribute))) {
                    var ordinal = reader.GetOrdinal(property.Name);

                    object value;
                    Type propType = property.PropertyType;

                    if (propType == typeof(bool)) {
                        value = reader.GetBoolean(ordinal);
                    } else if (propType == typeof(Int32)) {
                        value = reader.GetInt32(ordinal);
                    } else if (propType == typeof(DateTime)) {
                        value = reader.GetDateTime(ordinal);
                    } else if (propType == typeof(Item)) {
                        // TODO : Implement this 
                        value = DBNull.Value; 
                    } else {
                        value = reader.GetValue(ordinal);
                    }
                    
                    if (value != DBNull.Value) {
                        property.SetValue(instance, value, null);
                    }
                }
            }

            instance.Id = reader.GetGuid(reader.GetOrdinal("Id"));
        }

        private SQLiteCommand CreateCommand(string commandText) {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            return command;
        }

        public void SaveItem(Item item) {

            Debug.Assert(item != null);

            using (var tran = connection.BeginTransaction()) {

                using (var itemTypeCommand = GetCommand(itemTypeTable)) {
                    itemTypeCommand.Parameters["@Id"].Value = item.Id;
                    itemTypeCommand.Parameters["@Type"].Value = item.GetType().Name;
                    itemTypeCommand.ExecuteNonQuery();
                }

                foreach (var tableDef in GetTables(item.GetType())) {
                    var command = GetCommand(tableDef);
                    foreach (SQLiteParameter param in command.Parameters) {
                        if (param.ParameterName == "@Id" || param.ParameterName == "@ItemId") 
                        {
                            param.Value = item.Id;
                        } 
                        else 
                        {
                            string propertyName = param.ParameterName.Substring(1);
                            var property = item.GetType().GetProperty(propertyName);
                            object value = property.GetValue(item, null);
                            if (value is Item) {
                                value = (value as Item).Id;
                            }
                            param.Value = value; 
                        }
                    }
                    command.ExecuteNonQuery();
                }

                tran.Commit();
            }
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
