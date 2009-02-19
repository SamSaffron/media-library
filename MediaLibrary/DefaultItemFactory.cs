using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Helpers;
using Microsoft.Win32;

namespace MediaLibrary {
    class DefaultItemFactory : IItemFactory {

        

        public Item CreateItem(MediaLocation location, Library library) 
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
                    childFolderCount += childLocation is FolderMediaLocation ? 1 : 0;
                    // TODO: config setting
                    if (videoCount > 2 || childFolderCount > 0) break;
                }

                if (videoCount <= 2 && childFolderCount == 0) {
                    item = new Movie(library, location, null); 
                } else {
                    item = new Folder(library, location as FolderMediaLocation, null);
                }
            }
            else if (location.IsVideo() ) {
                return new Movie(library, location, null);  
            }

            return item;
        }

        public int Priority {
            get { return 0;  }
        }

    }
}