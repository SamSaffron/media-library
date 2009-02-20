using System;
using System.Collections.Generic;
using System.Text;
using MediaLibrary.Helpers;

namespace MediaLibrary
{
    delegate void ChildrenChangedEvent ();

    public class Folder : Item, IEnumerable<Item>
    {
        public Folder(Library library, IFolderMediaLocation  mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {
            children = new Lazy<List<Item>>(() => GetChildren(mediaLocation)); 
        }

        private Lazy<List<Item>> children; 
        public IList<Item> Children
        {
            get
            {
                return children.Value;
            }
        }

        public int UnwatchedCount { get; private set; }

        private List<Item> GetChildren(IFolderMediaLocation mediaLocation) {
            var children = new List<Item>();
            foreach (var child in mediaLocation.Children) {
                var item = Library.GetItem(child);
                if (item != null) {
                    item.Parent = this;
                    children.Add(item);
                }
            }
            return children;
        }

        public void Sort(SortOrder sortOrder)
        {
            children.Value.Sort(new ItemSorter(sortOrder)); 
        }

        ChildrenChangedEvent OnChildrenChanged;


        public Item this[int index] {
            get {
                return Children[index];
            }
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        System.Collections.IEnumerator  System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)Children).GetEnumerator();
        }


    }
}
