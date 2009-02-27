using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.ORM;

namespace MediaLibrary
{
    public class Configuration
    {

        private List<IMediaLocation> rootLocations = new List<IMediaLocation>();

        public static Configuration DefaultVideoLibraryConfig {
            get {
                var config = new Configuration();
                config.ItemFactories = new List<IItemFactory>() { new ItemFactory() };
                return config;
            }
        }

        public Configuration() {
            PluginPaths = new List<string>();
            MediaLocationFactory = new MediaLocationFactory();
            ValidateItems = true;
        }

        internal List<string> PluginPaths {get; private set; }

        public void AddPluginPath(string path) {
            PluginPaths.Add(path);
        }

        public void AddPlugin(IMetadataProvider provider) { 
        
        }

        public List<IItemFactory> ItemFactories { get; set; }

        public IMediaLocationFactory MediaLocationFactory { get; set; }

        public bool ValidateItems { get; set; } 

        public ItemRepository ItemRepository { get; set; }
        public List<IMediaLocation> RootLocations { get { return rootLocations; } }

        public string RootPath  
        {
            set
            {
                rootLocations.Clear();
                if (System.IO.Directory.Exists(value)) {
                    rootLocations.Add(new FolderMediaLocation(value, null));
                } else { 
                    rootLocations.Add(new MediaLocation(value, null));
                }
            }
        }

        
    }
}
