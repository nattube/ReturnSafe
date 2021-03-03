using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnSafe {
    /// <summary>
    /// An Exception that's thrown in case an Unwrap or Expect
    /// Method is used on an Option type that has no Value or 
    /// a Result type that has an Error
    /// </summary>
    public class UnwrapException : Exception {
        public UnwrapException() : base() { }
        public UnwrapException(string msg) : base(msg) { }
        public UnwrapException(string msg, Exception inner) : base(msg, inner) { }
    }
}
