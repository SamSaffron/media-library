using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary {
    internal class ItemSorter : IComparer<Item>  {

        SortOrder sortOrder; 

        public ItemSorter(SortOrder sortOrder) {
            this.sortOrder = sortOrder;
        }

        #region IComparer<Item> Members

        private int ProtectedComparison<T> (T x, T y) where T : IComparable {
            if (x == null || y == null) {
                if (x == null && y == null) {
                    return 0;
                }
                if (x == null) {
                    return -1;
                } else {
                    return 1;
                }
            }

            return x.CompareTo(y); 
        }

        public int Compare(Item x, Item y) {
            return ProtectedComparison(x.Name, y.Name);
        }

        #endregion
    }
}
