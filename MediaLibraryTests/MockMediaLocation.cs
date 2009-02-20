using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaLibrary;
using MediaLibrary.Helpers;

namespace MediaLibraryTests {
    class MockMediaLocation : IMediaLocation {

        public MockMediaLocation() { }

        public MockMediaLocation(string path) {
            this.Path = path;
        }

        public IFolderMediaLocation Parent {
            get;
            set;
        }

        public string Path {
            get;
            set;
        }

        public Guid Id {
            get {
                return Path.GetMD5();
            }
        }
    }
}
