using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary
{
    delegate void ChildrenChangedEvent ();

    public class Folder : Item, IEnumerable<Item>
    {
        public Folder(Library library, MediaLocation mediaLocation, Item parent) : 
            base(library, mediaLocation, parent) {

        }

        private List<Item> children; 
        public IList<Item> Children
        {
            get
            {
                EnsureChildrenLoaded();
                return children;
            }
        }

        private void EnsureChildrenLoaded() {
            if (children == null) {
                children = GetChildren();
            }
        }

        private List<Item> GetChildren() {
            var children = new List<Item>();
            foreach (var child in this.MediaLocation.Children) {
                var item = Library.CreateItem(child);
                if (item != null) {
                    item.Parent = this;
                    children.Add(item);
                }
            }
            return children;
        }

        public void Sort(SortOrder sortOrder)
        {
            EnsureChildrenLoaded();
            children.Sort(new ItemSorter(sortOrder)); 
        }

        ChildrenChangedEvent OnChildrenChanged;


        public Item this[int index] {
            get {
                return Children[index];
            }
        }

        public IEnumerator<Item>  GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        System.Collections.IEnumerator  System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)Children).GetEnumerator();
        }


    }
}
