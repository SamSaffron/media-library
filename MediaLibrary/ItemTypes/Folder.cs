using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    delegate void ChildrenChangedEvent ();

    public class Folder : Item
    {
        public Folder(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }

        private List<Item> children; 
        public IList<Item> Children
        {
            get
            {
                if (children == null) {
                    children = new List<Item>();
                    foreach (var child in this.MediaLocation.Children) {
                        var item = Library.CreateItem(child);
                        if (item != null) {
                            item.Parent = this;
                            children.Add(item);
                        }
                    }
                }

                return children;
            }
        }
        public void Sort(SortOrder sortOrder)
        { 
        }
        ChildrenChangedEvent OnChildrenChanged; 
    }
}
