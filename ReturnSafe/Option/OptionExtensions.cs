using ReturnSafe.Result;
using static ReturnSafe.Result.Result;
using static ReturnSafe.Option.Option;
using System;

namespace ReturnSafe.Option {
    public static class OptionExtensions {
        /// <summary>
        /// If option is Some the function will be executed and it's
        /// return value returned.
        /// Otherwise None will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="func">returns an option</param>
        /// <returns></returns>
        public static Option<T> AndThen<T>(this Option<T> option, Func<Option<T>> func) {
            if (option.IsSome) return func();
            return option;
        }

        /// <summary>
        /// If option is Some the function will be executed and it's
        /// return value returned.
        /// Otherwise None will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="func">takes in TSome and returns an Option</param>
        /// <returns></returns>
        public static Option<T> AndThen<T>(this Option<T> option, Func<T, Option<T>> func) {
            if (option.IsSome) return func(option.Unwrap());
            return option;
        }

        /// <summary>
        /// Returns the return value of func if the predicate returns true.
        /// Otherwise returns option unchanged.
        /// </summary>
        /// <returns></returns>
        public static Option<T> ExecuteIf<T>(this Option<T> option, Func<T, bool> predicate, Func<T, Option<T>> func) {
            if (option.IsSome && predicate(option.Unwrap())) return func(option.Unwrap());
            return option;
        }

        /// <summary>
        /// Executes func if option is Some and the predicate returns true.
        /// Than returns option unchanged.
        /// </summary>
        /// <returns></returns>
        public static Option<T> ExecuteIf<T>(this Option<T> option, Func<T, bool> predicate, Action<T> func) {
            if (option.IsSome && predicate(option.Unwrap())) func(option.Unwrap());
            return option;
        }

        /// <summary>
        /// Same as Unwrap but with your own detailed Error Message
        /// Returns the Value of the option or throws an Exception with 
        /// a specified error Message if option is None
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static T Expect<T>(this Option<T> option, string error) {
            if (option.IsSome) return option.Value;
            throw new UnwrapException(error);
        }

        /// <summary>
        /// Same as UnwrapError but with your own detailed Error Message
        /// Returns nothing if option is None or throws an Exception with 
        /// the specified error Message if option is Some
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="error"></param>
        public static void ExpectNone<T>(this Option<T> option, string error) {
            if (option.IsSome) throw new UnwrapException(error);
        }

        /// <summary>
        /// Returns a Result with an Ok value containing the options 
        /// value as long as option is Some.
        /// Otherwise a Result with Error(error) will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="option"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Result<T, TError> OkOr<T, TError>(this Option<T> option, TError error) {
            if (option.IsSome) return Ok(option.Unwrap());
            return Error(error);
        }

        /// <summary>
        /// Returns a Result with an Ok value containing the options 
        /// value as long as option is Some.
        /// Otherwise the specified result (alternative) will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="option"></param>
        /// <param name="alternative"></param>
        /// <returns></returns>
        public static Result<T, TError> OkOr<T, TError>(this Option<T> option, Result<T, TError> alternative) {
            if (option.IsSome) return Ok(option.Unwrap());
            return alternative;
        }

        /// <summary>
        /// Returns a Result with an Ok value containing the options 
        /// value as long as option is Some.
        /// Otherwise the return object of the specified function will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<T, TError> OkOrElse<T, TError>(this Option<T> option, Func<Result<T, TError>> func) {
            if (option.IsSome) return Ok(option.Unwrap());
            return func();
        }

        /// <summary>
        /// Return an Option with Some(value) if the predicate returns true.
        /// Otherwise an Option None will be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Option<T> OnlyIf<T>(this Option<T> option, Func<T, bool> predicate) {
            if (option.IsSome && predicate(option.Unwrap())) return option;
            return None();
        }

        /// <summary>
        /// If Option is Some the option will be returned.
        /// Otherwise the return object of the specified function will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Option<T> OrElse<T>(this Option<T> option, Func<Option<T>> func) {
            if (option.IsSome) return option;
            return func();
        }

        public static Option<TReturn> Select<T, TReturn>(this Option<T> option, Func<T, TReturn> func) {
            if(option.IsSome) return func(option.Unwrap());
            return None();
        }

        /// <summary>
        /// Same as Expect but without your own detailed Error Message
        /// Returns the Value of the option or throws an Exception if option is None
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <returns></returns>
        public static T Unwrap<T>(this Option<T> option) {
            if (option.IsSome) return option.Value;
            throw new UnwrapException();
        }

        /// <summary>
        /// Same as ExpectError but without your own detailed Error Message
        /// Returns nothing if option is None or throws an Exception if option is Some
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        public static void UnwrapNone<T>(this Option<T> option) {
            if (option.IsSome) throw new UnwrapException();
        }

        /// <summary>
        /// If Option is Some the Value will be returned.
        /// Otherwise the specified value (alternative) will be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="alternative"></param>
        /// <returns></returns>
        public static T UnwrapOr<T>(this Option<T> option, T alternative) {
            if (option.IsSome) return option.Value;
            return alternative;
        }

        /// <summary>
        /// If Option is Some the Value will be returned.
        /// Otherwise the return object of the specified function will be returned
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="option"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T UnwrapOrElse<T>(this Option<T> option, Func<T> func) {
            if (option.IsSome) return option.Value;
            return func();
        }
    }
}
