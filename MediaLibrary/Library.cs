using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Providers;
using MediaLibrary.Helpers;

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
            foreach (var location in config.RootLocations) {
                Item item = GetItem(location);
                if (item != null) {
                    items.Add(item);
                }
            }
            return items;
        }

        internal Item CreateItem(IMediaLocation location) {
            Item item = null;
            foreach (var factory in config.ItemFactories) {
                item = factory.CreateItem(location, this);
                if (item != null) break;
            }
            return item; 
        }

        public Item GetItem(IMediaLocation location)
        {
            Item item = null; 

            if (config.ItemRepository != null) {
                item = config.ItemRepository.GetItem<Item>(location.Id);
                if (config.ValidateItems && !item.IsValid()) {
                    item = null;
                }
            }

            if (item == null) {
                item = CreateItem(location);
            }

            return item;
        }

        public IMetadataProvider GetProvider(Type type)
        {
            throw new NotImplementedException();
        }

    }
}
