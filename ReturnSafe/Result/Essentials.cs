using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ReturnSafe.Result {
    public static class Result {
        /// <summary>
        /// Use: <code>Result&lt;TResult, TError&gt; myResult = Ok(myValue);</code>
        /// </summary>
        /// <param name="result">the value your result should carry</param>
        /// <returns>A result with the Value inside that returns true for result.isOk()</returns>
        public static Result<TResult> Ok<TResult>(TResult result) {
            return new Result<TResult>(result);
        }

        /// <summary>
        /// Use: <code>Result&lt;TResult, TError&gt; myResult = Error(myError);</code>
        /// </summary>
        /// <param name="error">the error</param>
        /// <returns>A result with the Error inside that returns false for result.isOk()</returns>
        public static Error<TError> Error<TError>(TError error) {
            return new Error<TError>(error);
        }

        /// <summary>
        /// Runs a function and catches Exceptions thrown by it and boxes them in an error result
        /// </summary>
        public static Result<TResult, Exception> Try<TResult>(Func<TResult> func) {
            try {
                TResult result = func();
                return Ok(result);
            }
            catch(Exception e) {
                return Error(e);
            }
        }

        /// <summary>
        /// Runs a function and returns a custom error if an Exception would be thrown.
        /// </summary>
        public static Result<TResult, TError> Try<TResult, TError>(Func<TResult> func, TError error) {
            try {
                TResult result = func();
                return Ok(result);
            }
            catch (Exception) {
                return Error(error);
            }
        }

        /// <summary>
        /// Runs a function that returns a task and catches Exceptions thrown by it and boxes them in an error result
        /// </summary>
        public static Result<TResult, Exception> TryAsync<TResult>(Func<Task<TResult>> awaitable) {
            try {
                TResult result = Task.Run(async () => await awaitable()).Result;
                return Ok(result);
            }
            catch (Exception e) {
                return Error(e);
            }
        }

        /// <summary>
        /// Runs a function that returns a task and returns a custom error if an exception would be thrown
        /// </summary>
        public static Result<TResult, TError> TryAsync<TResult, TError>(Func<Task<TResult>> awaitable, TError error) {
            try {
                TResult result = Task.Run(async () => await awaitable()).Result;
                return Ok(result);
            }
            catch (Exception) {
                return Error(error);
            }
        }

        /// <summary>
        /// Runs a function and catches Exceptions thrown by it and boxes them in an error result.
        /// It also returns an Error(NullpointerReferenceException) if func returns null
        /// </summary>
        public static Result<TResult, Exception> TryStrict<TResult>(Func<TResult> func) {
            try {
                TResult result = func();
                if (result == null) return Error((Exception)new NullReferenceException("func returned null"));
                return Ok(result);
            }
            catch (Exception e) {
                return Error(e);
            }
        }

        /// <summary>
        /// Runs a function and returns a custom error if an Exception would be thrown.
        /// It also returns an Error(TError) if func returns null
        /// </summary>
        public static Result<TResult, TError> TryStrict<TResult, TError>(Func<TResult> func, TError error) {
            try {
                TResult result = func();
                if (result == null) return Error(error);
                return Ok(result);
            }
            catch (Exception) {
                return Error(error);
            }
        }

        /// <summary>
        /// Runs a function that returns a task and catches Exceptions thrown by it and boxes them in an error result
        /// It also returns an Error(NullpointerReferenceException) if func returns null
        /// </summary>
        public static Result<TResult, Exception> TryStrictAsync<TResult>(Func<Task<TResult>> awaitable) {
            try {
                TResult result = Task.Run(async () => await awaitable()).Result;
                if(result == null) return Error((Exception)new NullReferenceException("func returned null"));
                return Ok(result);
            }
            catch (Exception e) {
                return Error(e);
            }
        }

        /// <summary>
        /// Runs a function that returns a task and catches Exceptions thrown by it and boxes them in an error result
        /// It also returns an Error(TError) if func returns null
        /// </summary>
        public static Result<TResult, TError> TryStrictAsync<TResult, TError>(Func<Task<TResult>> awaitable, TError error) {
            try {
                TResult result = Task.Run(async () => await awaitable()).Result;
                if (result == null) return Error(error);
                return Ok(result);
            }
            catch (Exception) {
                return Error(error);
            }
        }
    }
}
