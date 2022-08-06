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


    // Draft: Este programa é apenas um rascunho de possível trabalho futuro.
    public class Program
    {
        public static bool EndProgram { get; set; } = false;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");
            Console.WriteLine("💀💀💀    Road to DB - v2.0    💀💀💀");
            Console.WriteLine("💀💀💀         16-03-2022      💀💀💀");
            Console.WriteLine("💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀💀");


            Console.WriteLine("                                    _______          ");
            Console.WriteLine("                                   /       \\        ");
            Console.WriteLine("                               __ /   .-.  .\\        ");
            Console.WriteLine("                              /  `\\  /   \\/  \\       ");
            Console.WriteLine("                              | _ \\/   .==.==.\\           ");
            Console.WriteLine("                              |                 \\          ");
            Console.WriteLine("                             | (   \\ / ____\\__\\             ");
            Console.WriteLine("                              \\      (_()(_()                  ");
            Console.WriteLine("                               \\            '---.___             ");
            Console.WriteLine("                               \\                    \\               ");
            Console.WriteLine("                            /\\ |`       (__)________//                ");
            Console.WriteLine("                           /  \\|      /\\___/                 ");
            Console.WriteLine("                          |    \\     \\||VVV                  ");
            Console.WriteLine("                          |     \\    \\|_____                 ");
            Console.WriteLine("                          |      \\     ______)                   ");
            Console.WriteLine("                          \\       \\  /`                          ");
            Console.WriteLine("                                   \\(                                ");

            do
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\ifpno\Documents\GitHub\igor\projetos visual studio\Nightmarish 2.0\musica.wav");
                player.Play();
                ProgramOperations.ChooseOperation();


            }
            while (!EndProgram);
        }
    }
}
