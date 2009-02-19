using System;
using System.Collections.Generic;
namespace MediaLibrary {
    public interface IMediaLocation {
        FolderMediaLocation Parent { get; set; }
        string Path { get; set; }
    }
}
