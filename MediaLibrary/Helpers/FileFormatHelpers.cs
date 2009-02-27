using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Helpers {
    static class FileFormatHelpers {

        public static bool IsVirtualFolder(this string path) {
            return path.ToLower().EndsWith(".vf");
        }

        public static bool IsShortcut(this string path) {
            return path.ToLower().EndsWith(".lnk"); 
        }
    }
}
