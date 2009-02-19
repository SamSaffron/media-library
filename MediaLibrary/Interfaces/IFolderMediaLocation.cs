using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public interface IFolderMediaLocation : IMediaLocation {
        IList<IMediaLocation> Children { get; }
    }
}
