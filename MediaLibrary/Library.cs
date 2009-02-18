using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Providers;

namespace MediaLibrary
{
    public class Library
    {
        
        Configuration config;

        private Library(Configuration config) {
            this.config = config;
        }

        public static Library Initialize(Configuration config)
        {
            return new Library(config); 
        }

        public void Refresh() { 
        }

        public IList<Item> GetRootItems() {
            var items = new List<Item>();
            foreach (var path in config.RootPaths) {
                var location = new MediaLocation(path);
                Item item = CreateItem(location);
                if (item != null) {
                    items.Add(item);
                }
            }
            return items;
        }

        internal Item CreateItem(MediaLocation location) {
            Item item = null;
            foreach (var factory in config.ItemFactories) {
                item = factory.CreateItem(location, this);
                if (item != null) break;
            }
            return item; 
        }

        public Item GetItem(string path)
        {
            throw new NotImplementedException(); 
        }

        public IMetadataProvider GetProvider(Type type)
        {
            throw new NotImplementedException();
        }

    }
}
