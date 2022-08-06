using System;
using System.Collections;
using System.Collections.Generic;

namespace _3LoopsAndCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names; // can reference any array of strings

            // allocating memory for five strings in an array
            names = new string[5];

            // storing items at index positions
            names[0] = "Pedro";
            names[1] = "Jorge";
            names[2] = "Bernardo";
            names[3] = "Dora";
            names[4] = "Luís";

            // Abbreviated form:
            // string[] names = { "Pedro", "Jorge", "Carina", "Dora", "Luís" };

            // The same using a List
            List<string> namesList = new List<string>();
            namesList.Add("Catarina");
            namesList.Add("Nuno");
            namesList.Add("Nuno");
            namesList.Add("Marco");
            namesList.Add("Ricardo");

            // looping through the names using a for loop
            for (int i = 0; i < names.Length; i++)
            {
                // output the item at index position i
                Console.WriteLine(names[i]);

                Console.WriteLine(namesList[i]);
            }

            // looping through the names using a while loop
            int idx = 0;
            while (idx != names.Length)
            {
                Console.WriteLine(names[idx]);
                ++idx;
            }

            // looping through the names using a foreach loop, possible because 
            // List implements IEnumerable
            foreach (String s in names)
            {
                Console.WriteLine(s);
            }

            // Other useful classes in System.Collections.Generic namespace
            //  Dictionary: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2
            //  List: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1
            //  Queue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1
            //  SortedList: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2
            //  Stack: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1
        }
    }
}
