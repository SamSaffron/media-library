using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    public class Configuration
    {
        public static Configuration DefaultVideoLibraryConfig {
            get {
                var config = new Configuration();
                config.ItemFactories = new List<IItemFactory>() { new DefaultItemFactory() };
                return config;
            }
        }

        public Configuration() {
            PluginPaths = new List<string>();
        }

        internal List<string> PluginPaths {get; private set; }

        public void AddPluginPath(string path) {
            PluginPaths.Add(path);
        }

        public void AddPlugin(IMetadataProvider provider) { 
        
        }

        public List<IItemFactory> ItemFactories {
            get;
            set;
        }

        public IFolderMediaLocation[] RootLocations {
            get;
            set;
        }
    }
}
