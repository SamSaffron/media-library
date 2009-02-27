using System;
using System.Collections.Generic;
namespace MediaLibrary {
    public interface IMediaLocation {
        IFolderMediaLocation Parent { get; }
        string Path { get; }
        Guid Id {get; }
        string Contents { get; }
    }
}
