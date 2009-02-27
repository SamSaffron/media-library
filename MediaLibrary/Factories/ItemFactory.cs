using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Helpers;
using Microsoft.Win32;
using System.IO;

namespace MediaLibrary {
    class ItemFactory : IItemFactory {

        public Item CreateItem(IMediaLocation location, Library library) 
        {
            Item item = null;

            var folderLocation = location as IFolderMediaLocation;
            if (folderLocation != null) {

                // check for HDDVD,DVD,BluRay

                // check for movie 
                int videoCount = 0;
                int childFolderCount = 0;
                foreach (var childLocation in folderLocation.Children) {
                    videoCount += childLocation.IsVideo() ? 1 : 0;
                    childFolderCount += childLocation is IFolderMediaLocation ? 1 : 0;
                    // TODO: config setting
                    if (videoCount > 2 || childFolderCount > 0) break;
                }

                if (videoCount <= 2 && childFolderCount == 0) {
                    item = new Movie(); 
                } else {
                    var folder = new Folder();
                    folder.Location = folderLocation;
                    item = folder;  
                }
            }
            else if (location.IsVideo() ) {
                item = new Movie();  
            }

            if (item != null) {
                item.Library = library;
                item.Name = Path.GetFileNameWithoutExtension(location.Path);
                item.Uri = location.Path;
            }

            return item;
        }

        public int Priority {
            get { return 0;  }
        }

    }
}