using System;
using System.Collections.Generic;
namespace MediaLibrary {
    public interface IMediaLocation {
        IFolderMediaLocation Parent { get; set; }
        string Path { get; set; }
    }
}
