using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MediaLibrary.Helpers;

namespace MediaLibrary {
    public class FolderMediaLocation : MediaLocation, IFolderMediaLocation  {

        internal FolderMediaLocation(string path, FolderMediaLocation parent) 
            : base(path, parent) 
        {
            children = new Lazy<IList<IMediaLocation>>(GetChildren);
        }

        public IList<IMediaLocation> Children {
            get {
                return children.Value;
            }
        }

        #region private

        Lazy<IList<IMediaLocation>> children;

        private IList<IMediaLocation> GetChildren() {
            var children = new List<IMediaLocation>();
            foreach (var file in Directory.GetFiles(Path)) {

                // special handling for virtual folders and shortcuts goes here

                children.Add(new MediaLocation(file, this));
            }
            foreach (var dir in Directory.GetDirectories(Path)) {
                children.Add(new FolderMediaLocation(dir, this));
            }
            return children;
        }

        #endregion 
    }
}
