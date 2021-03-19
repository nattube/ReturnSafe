using System;
using ReturnSafe.Option;
using static ReturnSafe.Option.Option;
using ReturnSafe.Result;
using static ReturnSafe.Result.Result;

namespace ReturnSafe.Example {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello... I just realised I don't know your Name.");
            Console.WriteLine("May you help me?");
            Console.Write("First Name: ");
            
            Option<string> firstName = 
                HasGivenInput(Console.ReadLine())
                .OrElse(() => {
                    Console.WriteLine("Hello ..., Empty? no that sounds wrong! Don't try to fool me this time!");
                    Console.Write("First Name: ");
                    return HasGivenInput(Console.ReadLine());
                });

            //Alternative to: if(!firstName.isSome)
            if(!firstName) {
                Console.WriteLine("I see you don't wanna play, goodbye than!");
                return;
            }

            Console.WriteLine("Nice to meet you {0}!", firstName.Unwrap());

            firstName = firstName.ExecuteIf(
                (name) => name.Length > 7,
                (name) => {
                    Console.WriteLine("Your name is long indeed, do you want to tell me your nick name?");
                    Console.Write("nick name: ");
                    return HasGivenInput(Console.ReadLine())
                    .OrElse(() => name)
                    .AndThen((newName) => {
                        Console.WriteLine("OK! {0} it is!", newName);
                        return newName;
                    });
                });

            string name = firstName.UnwrapOr("'Should NOT be Possible to Trick me'");

            Console.WriteLine("Whats your age?");
            Result<string, Exception> saveResult = TryStrict(() => PotentiallyDangerousFunction(Console.ReadLine()));
            string ageInput = saveResult.OrElse(e => "'Bad Input'").Unwrap();

            Console.WriteLine("Even at your young age of only " + ageInput + " years, you should now know how to return safe " + name + "!");
            Console.WriteLine("Thanks for playing!");
        }

        static string PotentiallyDangerousFunction(string input) {
            if (String.IsNullOrEmpty(input)) return null; //possible Null return            
            int i = int.Parse(input); //Possible Exception not handled
            return input;
        }

        static Option<string> HasGivenInput(string firstName) {
            if (String.IsNullOrWhiteSpace(firstName))
                return None();
            return Some(firstName);
        }
    }
}
