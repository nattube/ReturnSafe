# ReturnSafe  
[![nuget.org](https://img.shields.io/nuget/v/ReturnSafe?color=blue)](https://www.nuget.org/packages/ReturnSafe/)

A Library that brings Rusts Option and Result Types to C#

## How is it Helpfull?
The Library makes errors in functions more explicit. This helps reducing Errors caused by Exceptions and Null Pointers.
It does not prevent you from producing an error due to a Nullpointer or an unexpected Exception, but it helps you with
pointing out that there could be something wrong and that one should check the result.

## What does it contain?
### Option\<TOption\>
An Option can either be of Some(TOption) or None. Its basicaly the explicit form of returning a Value or Null.
There are severel extension Methods for Option that let you operate on the option types, unwrap them or convert them.

### Result<TResult, TError>
A Result can either be Ok(TResult) or Error(TError). This helps you with handling different possible Errors in your Functions and to communicate them to the caller.
There are severel extension Methods for Result that let you operate on the result types, unwrap them or convert them.

## Usage
#### Note: For a more detailed Example checkout the ReturnSafe.Example folder

make sure to import the Essentials as static to use the convenience functions
```c#
using ReturnSafe.Option;
using static ReturnSafe.Option.Essentials; //Import Convenience Functions like Some(TOptione) and None()
using ReturnSafe.Result;
using static ReturnSafe.Result.Essentials; //Import Convenience Functions like Ok(TResult) and Error(TError)
```

after that you can create functions that return Results or Options like this: 
```c#
public Result<int, String> StringToIntResult(string value) {
    if (value == null) 
        return Error("NULL Error"); 
    
    int i = 0;

    Result<int, Exception> safeResult = TryStrict(() => int.Parse(value)); // Make external functions Save    
    int i2 = safeResult.OrElse(x => x is NullReferenceException ? -2 : -1).Unwrap(); // Provide an alternative value dependant on the error


    if (!int.TryParse(value, out i)) 
        return Error("Format Error"); // Error Returns need to be Explicit

    if (i2 != i)
        return Error("There is clearly something wrong");

    if (i < 0) 
        return i * -1; //Ok Returns can be Implicit

    return Ok(i); // or explicit
}

public Option<int> StringToIntOption(string value) {
    if (value == null)
        return None();

    int i = 0;

    if (!int.TryParse(value, out i))
        return None(); // None Returns need to be Explicit

    if (i < 0)
        return i * -1; //Some Returns can be Implicit

    return Some(i); // or explicit
}
```

you can work on this return types in many different ways. You can either check the results with an if statement and unwrap them directly

```c#
Option<int> myInt = StringToIntOption("12");
if(myInt) // == if(myInt.isSome) 
{
    int i = myInt.Unwrap(); // throws an UnwrapException if myInt == None
}
   
```

or you can use some of the extension Methods that let you work on the result or option values with lambda functions. For example:

```c#
Result<int, String> saveResult = StringToIntResult(Console.ReadLine());
int ageInput = saveResult.OrElse(error => -1).Unwrap(); /* The Lambda function in OrElse is only called on Errors and returns a new Result, if saveResult is Ok saveResult stays unchanged */
```
Make sure to checkout the other Methods like `AndThen` or `ExecuteIf`.
