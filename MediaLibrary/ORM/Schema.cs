using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using MediaLibrary.ORM.Attributes;

namespace MediaLibrary.ORM {

    public class Schema : List<TableDefinition> {

        public static Schema DiscoverSchema() {
            return DiscoverSchema(new Assembly[] { Assembly.GetExecutingAssembly() }); 
        }

        public static Schema DiscoverSchema(Assembly[] assemblies) {
            var schema = new Schema();
            foreach (var assembly in assemblies)
            foreach (var type in assembly.GetTypes()) 
            {
                var def = ExtractTableDefinition(type);
                if (def != null) 
                {
                    if (def.ColumnDefinitions.Count > 1) {
                        schema.Add(def);
                    }
                    schema.MappedTypes[type.Name] = type;
                }
            }
            return schema;
        }

        private static TableDefinition ExtractTableDefinition(Type type) {
            if (!IsItem(type)) return null;

            var tableDefinition = new TableDefinition(type.Name);

            if (type != typeof(Item)) {
                tableDefinition.AddColumn("ItemId", typeof(Guid), ColumnProperties.PrimaryKey);
            }

            foreach (var property in type.GetProperties()) {

                // This can be optimised 
                bool found = false;
                foreach (var parentProperty in type.BaseType.GetProperties())
	            {
                    if (parentProperty.Name == property.Name) {
                        found = true;
                        break;
                    }
	            }
                if (found) continue;

                if (HasAttribute(property, typeof(ColumnAttribute))) {
                    tableDefinition.AddColumn(property.Name, property.PropertyType);
                } else if (HasAttribute(property, typeof(PrimaryKeyAttribute))) {
                    tableDefinition.AddColumn(property.Name, property.PropertyType, ColumnProperties.PrimaryKey);
                }
            }
            return tableDefinition;
            
        }

        internal static bool HasAttribute(PropertyInfo info, Type type) { 
            foreach (var attribute in info.GetCustomAttributes(false)) {
                if (attribute.GetType() ==  type) {
                    return true;
                }
            }
            return false;
        }

      

        private static bool IsItem(Type type) {
            return type.IsSubclassOf(typeof(Item)) || type == typeof(Item); 
        }

        private Schema ()
	    {

	    }

        public Dictionary<string, Type> MappedTypes = new Dictionary<string, Type>();
    }
}
