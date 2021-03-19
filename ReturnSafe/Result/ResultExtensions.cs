using ReturnSafe.Option;
using static ReturnSafe.Option.Option;
using static ReturnSafe.Result.Result;
using System;

namespace ReturnSafe.Result {
    public static class ResultExtensions {
        /// <summary>
        /// If result is Ok the function will be executed and it's
        /// return value returned.
        /// Otherwise the result will be returned unchanged
        /// </summary>
        public static Result<TResult, TError> AndThen<TResult, TError>(this Result<TResult, TError> result, Func<TResult, Result<TResult, TError>> func) {
            if (result.IsOk) return func(result.Unwrap());
            return result;
        }

        /// <summary>
        /// If result is Ok the provided function will be executed and it's
        /// return value returned.
        /// Otherwise the result will be returned unchanged
        /// </summary>
        public static Result<TResult, TError> AndThen<TResult, TError>(this Result<TResult, TError> result, Func<Result<TResult, TError>> func) {
            if (result.IsOk) return func();
            return result;
        }

        /// <summary>
        /// If result is Ok and predicate is true func will be executed and its 
        /// return value returned.
        /// Otherwise result will be returnded unchanged.
        /// </summary>
        public static Result<TResult, TError> ExecuteIf<TResult, TError>(this Result<TResult, TError> result, Func<TResult, bool> predicate, Func<TResult, Result<TResult, TError>> func) {
            if (result.IsOk && predicate(result.Unwrap())) return func(result.Unwrap());
            return result;
        }

        /// <summary>
        /// If result is Ok and predicate is true func will be executed.
        /// Than result will be returnded unchanged.
        /// </summary>
        public static Result<TResult, TError> ExecuteIf<TResult, TError>(this Result<TResult, TError> result, Func<TResult, bool> predicate, Action<TResult> func) {
            if (result.IsOk && predicate(result.Unwrap())) func(result.Unwrap());
            return result;
        }

        /// <summary>
        /// Same as Unwrap but with your own detailed Error Message
        /// If result is Ok it's value will be returned
        /// Otherwise an UnwrapException with your own Error Message will
        /// be thrown
        /// </summary>
        public static TResult Expect<TResult, TError>(this Result<TResult, TError> result, string error) {
            if (result.IsOk) return result.Value;
            throw new UnwrapException(error);
        }

        /// <summary>
        /// Same as UnwrapError but with your own detailed Error Message
        /// If result is Error it's Error value will be returned
        /// Otherwise an UnwrapException with your own Error Message will
        /// be thrown
        /// </summary>
        public static TError ExpectError<TResult, TError>(this Result<TResult, TError> result, string error) {
            if (result.IsOk) throw new UnwrapException(error);
            return result.Error;            
        }

        /// <summary>
        /// If Result is Ok and it's value matches the predicate it will be returned as
        /// Ok(result)!
        /// If result is Ok and it's value DOES NOT match the predicate a Result with
        /// Error(yourError) will be returned!
        /// If result is an Error it will be returnded unchanged
        /// </summary>
        public static Result<TResult, TError> IfNot<TResult, TError>(this Result<TResult, TError> result, Func<TResult, bool> predicate, TError error) {
            if (result.IsOk && predicate(result.Unwrap())) return result;
            else if (result.IsError) return result;
            return Error(error);
        }

        /// <summary>
        /// If result is Ok it will be returned unchanged!
        /// Otherwise the return value of your specified function
        /// will be returned!
        /// </summary>
        public static Result<TResult, TError> OrElse<TResult, TError>(this Result<TResult, TError> result, Func<Result<TResult, TError>> func) {
            if (result.IsOk) return result;
            return func();
        }

        /// <summary>
        /// If result is Ok it will be returned unchanged!
        /// Otherwise the return value of your specified function
        /// will be returned!
        /// </summary>
        public static Result<TResult, TError> OrElse<TResult, TError>(this Result<TResult, TError> result, Func<TError, Result<TResult, TError>> func) {
            if (result.IsOk) return result;
            return func(result.Error);
        }


        /// <summary>
        /// If result is Error and the predicate is true the return value of your function will be returned!
        /// Otherwise result stays unchanged.
        /// </summary>
        public static Result<TResult, TError> OrElseIf<TResult, TError>(this Result<TResult, TError> result, Func<TError, bool> predicate, Func<Result<TResult, TError>> func) {
            if (result.IsError && predicate(result.UnwrapError())) return func();
            return result;
        }

        /// <summary>
        /// If result is Error and the predicate is true the return value of your function will be returned!
        /// Otherwise result stays unchanged.
        /// </summary>
        public static Result<TResult, TError> OrElseIf<TResult, TError>(this Result<TResult, TError> result, Func<TError, bool> predicate, Func<TError, Result<TResult, TError>> func) {
            if (result.IsError && predicate(result.UnwrapError())) return func(result.UnwrapError());
            return result;
        }

        /// <summary>
        /// If result is Ok an option with the results value will be returned!
        /// Otherwise the alternative value will be returned as Option Some(alternative)
        /// </summary>
        public static Option<TResult> SomeOr<TResult, TError>(this Result<TResult, TError> result, TResult alternative) {
            if (result.IsOk) return Some(result.Unwrap());
            return Some(alternative);
        }

        /// <summary>
        /// If result is Ok an option with the results value will be returned!
        /// Otherwise the alternative option will be returned. 
        /// Example1:
        /// <code>result.SomeOr(None());</code>
        /// Example2:
        /// <code>result.SomeOr(Some(value));</code>
        /// </summary>
        public static Option<TResult> SomeOr<TResult, TError>(this Result<TResult, TError> result, Option<TResult> alternative) {
            if (result.IsOk) return Some(result.Unwrap());
            return alternative;
        }

        /// <summary>
        /// If result is Ok an option with the results value will be returned!
        /// Otherwise the return value of the specified function will be returned
        /// </summary>
        public static Option<TResult> SomeOrElse<TResult, TError>(this Result<TResult, TError> result, Func<Option<TResult>> func) {
            if (result.IsOk) return Some(result.Unwrap());
            return func();
        }

        /// <summary>
        /// If result is Ok an option with the results value will be returned!
        /// Otherwise the return value of the specified function will be returned!
        /// Here the function takes the Error Value as input
        /// </summary>
        public static Option<TResult> SomeOrElse<TResult, TError>(this Result<TResult, TError> result, Func<TError, Option<TResult>> func) {
            if (result.IsOk) return Some(result.Unwrap());
            return func(result.Error);
        }

        /// <summary>
        /// Same as Expect but without your own detailed Error Message
        /// If result is Ok it's value will be returned
        /// Otherwise an UnwrapException will be thrown
        /// </summary>
        public static TResult Unwrap<TResult, TError>(this Result<TResult, TError> result) {
            if (result.IsOk) return result.Value;
            throw new UnwrapException();
        }

        /// <summary>
        /// Same as ExpectError but without your own detailed Error Message
        /// If result is Error it's Error value will be returned
        /// Otherwise an UnwrapException will be thrown
        /// </summary>
        public static TError UnwrapError<TResult, TError>(this Result<TResult, TError> result) {
            if (result.IsOk) throw new UnwrapException();
            return result.Error;
        }

        /// <summary>
        /// If result is Ok it's value will be returned!
        /// Otherwise your specified alternative will be returned
        /// </summary>
        public static TResult UnwrapOr<TResult, TError>(this Result<TResult, TError> result, TResult alternative) {
            if (result.IsOk) return result.Value;
            return alternative;
        }

        /// <summary>
        /// If result is Ok it's value will be returned!
        /// Otherwise the returne value of your specified function will be returned
        /// </summary>
        public static TResult UnwrapOrElse<TResult, TError>(this Result<TResult, TError> result, Func<TResult> func) {
            if (result.IsOk) return result.Value;
            return func();
        }

        /// <summary>
        /// If result is Ok it's value will be returned!
        /// Otherwise the returne value of your specified function will be returned!
        /// Here the function takes TError as input
        /// </summary>
        public static TResult UnwrapOrElse<TResult, TError>(this Result<TResult, TError> result, Func<TError, TResult> func) {
            if (result.IsOk) return result.Value;
            return func(result.Error);
        }
    }
}
