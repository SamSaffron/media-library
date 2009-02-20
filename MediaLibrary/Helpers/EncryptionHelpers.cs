using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace MediaLibrary.Helpers {
    public static class EncryptionHelpers {

        static MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

        public static Guid GetMD5(this string str) {
            return new Guid(md5Provider.ComputeHash(Encoding.Unicode.GetBytes(str))); 
        }
    }
}
