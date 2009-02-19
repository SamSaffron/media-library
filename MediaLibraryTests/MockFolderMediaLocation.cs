using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaLibrary;

namespace MediaLibraryTests {
    class MockFolderMediaLocation : IFolderMediaLocation {

        public static MockFolderMediaLocation[] CreateLocation(string config) {
            return null;
        }

        public IList<MediaLocation> Children {
            get { throw new NotImplementedException(); }
        }


        public FolderMediaLocation Parent {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public string Path {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
