using System;
using System.Runtime.CompilerServices;

namespace ReturnSafe.Result {
    /// <summary>
    /// Internal Helper Class that needs public visibility but should never be used Outside
    /// </summary>
    public class Result<TResult> {
        private TResult _value;

        internal TResult Value => this._value;

        internal Result(TResult result) {
            this._value = result;
        }
    }

    /// <summary>
    /// Internal Helper Class that needs public visibility but should never be used Outside
    /// </summary>
    public class Error<TError> {
        private TError _value;

        internal TError Value => this._value;

        internal Error(TError error) {
            this._value = error;
        }
    }

    /// <summary>
    /// Result can either carry a Value(Ok) or an error-value(Error).
    /// This class can help you to avoid common Errors with Null
    /// and Exception handling. 
    /// It also has severel useful extension Methods to help you
    /// with control flow. Use <code>Ok(TResult);</code> or <code>Error(TError);</code>
    /// for construction.  
    /// Receive the value or Error with one of the functions from ResultExtensions. For example you could use
    /// <code>result.Unwrap();</code>
    /// to get the value and
    /// <code>result.UnwrapError();</code>
    /// to get the Error
    /// </summary>
    /// <typeparam name="TResult">The type of the Result</typeparam>
    /// <typeparam name="TError">The type of the Error</typeparam>
    public struct Result<TResult, TError> {
        private TResult _value;
        private TError _error;
        private bool _isOk;

        internal TResult Value => _value;
        internal TError Error => _error;

        public bool IsOk => _isOk;
        public bool IsError => !_isOk;
        
        private Result(TError error) {
            this._error = error;
            this._isOk = false;
            this._value = default(TResult);
        }
        private Result(TResult result) {
            this._error = default(TError);
            this._isOk = true;
            this._value = result;
        }

        /// <summary>
        /// Enable <code>if(result){...}</code> like style instead of <code>if(result.IsOk) {...}</code>
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator bool(Result<TResult, TError> result) => result.IsOk;

        public static implicit operator Result<TResult, TError>(TResult result) => new Result<TResult, TError>(result);
        public static implicit operator Result<TResult, TError>(Result<TResult> result) => new Result<TResult, TError>(result.Value);
        public static implicit operator Result<TResult, TError>(Error<TError> error) => new Result<TResult, TError>(error.Value);
    }
}
