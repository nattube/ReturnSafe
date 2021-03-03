using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnSafe.Option {

    /// <summary>
    /// Internal Helper Class that needs public visibility but should never be used Outside
    /// </summary>
    public class None {
        internal None() { }
    }

    /// <summary>
    /// Option can either carry a Value(Some) or Not(None).
    /// This class can help you to avoid common Errors with Null
    /// It also has severel useful extension Methods to help you
    /// with control flow. Use <code>Some(TResult);</code> or <code>None();</code>
    /// for construction.
    /// Receive the value with one of the functions from OptionExtensions. For example you could use
    /// <code>option.Unwrap();</code>
    /// to get the value
    /// </summary>
    /// <typeparam name="T">the type of the Value</typeparam>
    public struct Option<T> {
        private T _value;
        private bool _isSome;

        internal T Value => _value;

        public bool IsSome => _isSome;
        public bool IsNone => !_isSome;

        internal Option(None none) {
            this._isSome = false;
            this._value = default(T);
        }
        internal Option(T result) {
            this._isSome = true;
            this._value = result;
        }

        public static implicit operator bool(Option<T> option) => option.IsSome;

        public static implicit operator Option<T>(T value) => new Option<T>(value);
        public static implicit operator Option<T>(None none) => new Option<T>(none);
    }
}
