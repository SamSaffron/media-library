using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    public interface IItemFactory {
        Item CreateItem(MediaLocation location, Library library);
       
        // TODO: Consider adding priority if required
        // int Priority { get; }
    }
}
