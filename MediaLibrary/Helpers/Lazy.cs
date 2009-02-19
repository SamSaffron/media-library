using System;
using System.Collections.Generic;
using System.Text;

namespace MediaLibrary.Helpers {

    // comment me out for .net 3.5
    internal delegate TResult Func<TResult>();  

    internal class Lazy<T> {
        
        private Func<T> func;
        private T result;
        private bool hasValue;


        public Lazy(Func<T> func) {
            this.func = func;
            this.hasValue = false;
        }


        public T Value {
            get {
                lock (func) {
                    if (!this.hasValue) {
                        this.result = this.func();
                        this.hasValue = true;
                    }
                }
                return this.result;
            }
        }
    }
}
