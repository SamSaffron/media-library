using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaLibrary.Helpers;

namespace MediaLibrary {
    public class FolderMediaLocation : MediaLocation, IFolderMediaLocation  {


        internal FolderMediaLocation(string path, IFolderMediaLocation parent) 
            : this(path, parent, null) 
        {
        }

        // special constructor used by the virtual folders (allows for folder relocation)
        internal FolderMediaLocation(string path, IFolderMediaLocation parent, IFolderMediaLocation location)
            : base(path, parent) {
            children = new Lazy<IList<IMediaLocation>>(GetChildren);
            if (location == null) {
                this.location = this;
            } else {
                this.location = location;
            }
        }


        public IList<IMediaLocation> Children {
            get {
                return children.Value;
            }
        }

        #region private

        private IFolderMediaLocation location; 
        Lazy<IList<IMediaLocation>> children;

        private IList<IMediaLocation> GetChildren() {
            var children = new List<IMediaLocation>();
            foreach (var file in Directory.GetFiles(Path)) {

                var resolved = file; 

                if (file.IsShortcut()) {
                    resolved = ShortcutResolver.Resolve(file);
                }

                if (resolved.IsVirtualFolder()) {
                    children.Add(new VirtualFolderMediaLocation(resolved, location)); 
                }  
                else {
                    if (resolved != file && Directory.Exists(resolved)) {
                        children.Add(new FolderMediaLocation(resolved, location));
                    } else {
                        children.Add(new MediaLocation(resolved, location));
                    }
                }
            }

            foreach (var dir in Directory.GetDirectories(Path)) {
                children.Add(new FolderMediaLocation(dir, location));
            }
            return children;
        }

        #endregion 
    }
}
