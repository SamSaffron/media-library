using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaLibrary;

namespace MediaLibraryTests {
    class MockFolderMediaLocation :  MockMediaLocation, IFolderMediaLocation {

        class RowInfo {
            public RowInfo(string line) {
                Depth = line.Length - line.TrimStart().Length;
                var trimmedLine = line.TrimStart();
                IsFolder = trimmedLine.StartsWith("|");
                Path = IsFolder ? trimmedLine.Substring(1) : trimmedLine;  
            }

            public int Depth { get; private set; }
            public bool IsFolder { get; private set; }
            public string Path { get; private set; }
        }

        class Builder {

            private MockFolderMediaLocation location = new MockFolderMediaLocation();

            public Builder() {
                location.Path = "";
            }

            public void AddParent(RowInfo info,int depth) {
                while (depth > 0) {
                    depth--;
                    location = (MockFolderMediaLocation)location.Parent;
                }
                AddSibling(info);
            }
            public void AddChild(RowInfo info) {
                location = (MockFolderMediaLocation)location.Children.Last();
                AddSibling(info);
            }
            public void AddSibling(RowInfo info) {
                MockMediaLocation newLocation;
                if (info.IsFolder) {
                    newLocation = new MockFolderMediaLocation();
                } else {
                    newLocation = new MockMediaLocation(); 
                }
                if (location.Path.Length > 0) {
                    newLocation.Path = location.Path + "\\" + info.Path;
                } else {
                    newLocation.Path = info.Path;
                }
                newLocation.Parent = this.location;
                location.Children.Add(newLocation);
            }

            public MockFolderMediaLocation[] RootFolders {
                get {
                    var root = location;
                    while (true) {
                        if (root.Parent != null) {
                            root = (MockFolderMediaLocation)root.Parent;
                        } else {
                            break;
                        }
                    }

                    var rval = new List<MockFolderMediaLocation>();

                    foreach (var item in root.Children) {
                        if (item is MockFolderMediaLocation) {
                            rval.Add(item as MockFolderMediaLocation); 
                        }
                    }

                    return rval.ToArray();
                }
            }
        }

        public static MockFolderMediaLocation[] CreateMockLocations(string config) {

            var builder = new Builder();
            var depth = 0;

            foreach (var line in config.Split(new string[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries)) 
                {
                    var rowInfo = new RowInfo(line);

                    if (rowInfo.Depth == depth) {
                        builder.AddSibling(rowInfo); 
                    } else if (rowInfo.Depth > depth) {
                        builder.AddChild(rowInfo);
                    } else {
                        builder.AddParent(rowInfo, depth - rowInfo.Depth);
                    }

                    depth = rowInfo.Depth;
                }

            return builder.RootFolders;
        }

        private IList<IMediaLocation> children = new List<IMediaLocation>();
        public IList<IMediaLocation> Children {
            get { return children; }
        }

    }
}
