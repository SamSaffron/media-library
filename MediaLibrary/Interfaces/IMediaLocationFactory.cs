using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public interface IMediaLocationFactory {
        IMediaLocation CreateMediaLocation(string path, Library library);
    }
}
