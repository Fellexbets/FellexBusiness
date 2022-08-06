using System;
using System.Text;

namespace RoadToDB
{
    // More info: 
    //      Attribute:   https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/
    //      Foreach:     https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/make-class-foreach-statement
    //      IEnumerable: https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-5.0
    //      Generics:    https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
    //      Enumeration: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum
    //      Delegates: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/
    //      Dictionary<TKey,TValue> Class: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-6.0
    //      Reflection: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/reflection
    //      JSON configuration Provider: https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers#json-configuration-provider
    //      Asynchronous programming: https://docs.microsoft.com/en-us/dotnet/csharp/async
    //      Nullable Contexts: https://docs.microsoft.com/en-us/dotnet/csharp/nullable-references#nullable-contexts
    //      Try-catch: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-catch
    //      ChangeType(Object, Type): https://docs.microsoft.com/en-us/dotnet/api/system.convert.changetype?view=netcore-3.1#system-convert-changetype(system-object-system-type)
    //      Nullable.GetUnderlyingType(Type) Method https://docs.microsoft.com/en-us/dotnet/api/system.nullable.getunderlyingtype?view=net-6.0

    // Draft (22-03-2022): Este programa é apenas um rascunho de possível trabalho futuro.
    public class Program
    {
        public static bool EndProgram { get; set; } = false;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********************************");
            Console.WriteLine("***      Road to DB - v2.0     ***");
            Console.WriteLine("***         16-03-2022         ***");
            Console.WriteLine("**********************************");

            do
            {
                try
                {
                    ProgramOperations.ChooseOperation();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }
            while (!EndProgram);
        }
    }
}
