using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaLibrary;

namespace MediaLibraryTests {
    class MockMediaLocation : IMediaLocation {

        public IFolderMediaLocation Parent {
            get;
            set;
        }

        public string Path {
            get;
            set;
        }
    }
}
