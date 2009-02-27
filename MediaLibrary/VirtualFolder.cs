using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MediaLibrary {
    class VirtualFolder {
        
        List<string> folders = new List<string>();
        string image;

        public VirtualFolder(string contents) {
            // splitting on \n cause I want this to work for VFs edited in linux. 
            foreach (var line in contents.Split('\n')) {
                var colonPos = line.IndexOf(':');
                if (colonPos <= 0) {
                    continue;
                }

                var type = line.Substring(0, colonPos);
                var filename = line.Substring(colonPos + 1).Trim();

                if (type == "image") {
                    image = filename;
                } else if (type == "folder") {
                    folders.Add(filename);
                }

            }
        }

        public List<string> Folders { get { return folders; } }

        public string ImagePath {
            get { return image; }
        }
    
    }
}
