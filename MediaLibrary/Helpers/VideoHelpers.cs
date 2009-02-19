using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace MediaLibrary.Helpers {
    static class VideoHelpers {

        #region Known video extensions
        public static Dictionary<string, bool> videoExtensions =
            new Dictionary<string, bool>() {
                {".vob", false},
                {".bup", false}, 
                {".ifo", false}, 
                {".rmvb",true},
                {".mov",true},
                {".avi",true},
                {".mpg",true},
                {".mpeg",true},
                {".wmv",true},
                {".mp4",true},
                {".mkv",true},
                {".divx",true},
                {".dvr-ms",true},
                {".ogm",true}
            };
        #endregion 

        public static bool IsVideo(string path) {

            string extension = System.IO.Path.GetExtension(path).ToLower();

            bool isVideo;
            if (videoExtensions.TryGetValue(extension, out isVideo)) {
                return isVideo;
            }

            string pt = null;
            RegistryKey key = Registry.ClassesRoot;
            key = key.OpenSubKey(extension);
            if (key != null) {
                pt = key.GetValue("PerceivedType") as string;
            }
            if (pt == null) pt = "";
            pt = pt.ToLower();

            lock (videoExtensions) {
                videoExtensions[extension] = (pt == "video");
            }

            return videoExtensions[extension];


        }


        #region MediaLocation extensions

        public static bool IsVideo(this MediaLocation location) {
            return IsVideo(location.Path);
        }

        #endregion
    }

}
