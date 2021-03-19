using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnSafe.Option {
    public static class Option {
        /// <summary>
        /// Use: <code>Option&lt;TResult&gt; myOption = Some(myValue);</code>
        /// </summary>
        /// <param name="result">the value your option should carry</param>
        /// <returns>An option with the Value inside that returns true for option.isSome()</returns>
        public static Option<TResult> Some<TResult>(TResult result) {
            if(result == null)
                return None();
            return new Option<TResult>(result);
        }

        /// <summary>
        /// Use: <code>Option&lt;TResult&gt; myOption = None();</code>
        /// </summary>
        /// <returns>An option with no value that returns false for option.isSome()</returns>
        public static None None() {
            return new None();
        }

        /// <summary>
        /// returns the result of the passed function or None() if the function returns null
        /// </summary>
        public static Option<TResult> ResolveNullable<TResult>(Func<TResult> nullableFunction) {
            TResult result = nullableFunction();
            return result == null ? None() : Some(result);
        }
    }
}
