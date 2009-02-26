using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.ORM;

namespace MediaLibrary
{
    public class Configuration
    {

        private List<IFolderMediaLocation> rootLocations = new List<IFolderMediaLocation>();

        public static Configuration DefaultVideoLibraryConfig {
            get {
                var config = new Configuration();
                config.ItemFactories = new List<IItemFactory>() { new DefaultItemFactory() };
                return config;
            }
        }

        public Configuration() {
            PluginPaths = new List<string>();
            ValidateItems = true;
        }

        internal List<string> PluginPaths {get; private set; }

        public void AddPluginPath(string path) {
            PluginPaths.Add(path);
        }

        public void AddPlugin(IMetadataProvider provider) { 
        
        }

        public List<IItemFactory> ItemFactories { get; set; }

        public bool ValidateItems { get; set; } 

        public ItemRepository ItemRepository { get; set; }
        public List<IFolderMediaLocation> RootLocations { get { return rootLocations; } }

        public void AddRootPath(string path) {
            rootLocations.Add(new FolderMediaLocation(path, null));
        }

        
    }
}
