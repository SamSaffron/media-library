using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace MediaLibrary {
    class DefaultItemFactory : IItemFactory {

        public static Dictionary<string, bool> perceivedTypeCache = new Dictionary<string, bool>();

        public Item CreateItem(MediaLocation location, Library library) {
            if (location.IsFolder) {
                return new Folder(library, location, null); 
            }

            if (IsVideo(location.Path)) {
                return new Movie(library, location, null);  
            }
            return null;
        }

        public int Priority {
            get { return 0;  }
        }


        // I left the hardcoded list, cause the failure mode is better, at least it will show
        // videos if the codecs are not installed properly
        private static bool IsVideo(string filename) {
            //using (new Profiler(filename))
            {
                string extension = System.IO.Path.GetExtension(filename).ToLower();

                switch (extension) {
                    // special case so DVD files are never considered videos
                    case ".vob":
                    case ".bup":
                    case ".ifo":
                        return false;
                    case ".rmvb":
                    case ".mov":
                    case ".avi":
                    case ".mpg":
                    case ".mpeg":
                    case ".wmv":
                    case ".mp4":
                    case ".mkv":
                    case ".divx":
                    //case ".iso": // these are not directly playable and need to be handled differently
                    case ".dvr-ms":
                    case ".ogm":
                        return true;

                    default:

                        bool isVideo;
                        lock (perceivedTypeCache) {
                            if (perceivedTypeCache.TryGetValue(extension, out isVideo)) {
                                return isVideo;
                            }
                        }

                        string pt = null;
                        RegistryKey key = Registry.ClassesRoot;
                        key = key.OpenSubKey(extension);
                        if (key != null) {
                            pt = key.GetValue("PerceivedType") as string;
                        }
                        if (pt == null) pt = "";
                        pt = pt.ToLower();

                        lock (perceivedTypeCache) {
                            perceivedTypeCache[extension] = (pt == "video");
                        }

                        return perceivedTypeCache[extension];
                }
            }
        }
   
    
    }
}