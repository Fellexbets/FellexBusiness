using System;
using System.Linq;
using System.Collections.Generic;

namespace _6Linq
{
    class Program
    {
        // SELECT, UPDATE, DELETE, INSERT
        static void Main(string[] args)
        {
            string[] names = { "Anabela", "Ricardo", "Ricardo" };

            // Desafio:
            //      1. Seleccionar nomes com a letra "a"
            IEnumerable<string> namesWithLetterA = names.Where(name => name.ToLower().Contains("a"));
            
            //      2. Ordenar por ordem crescente do número de caracteres
            IEnumerable<string> orderedNames = from name in namesWithLetterA
                                               orderby name.Length
                                               select name;
                                            
            // namesWithLetterA.OrderBy(name => name.Length);
            
            //      3. Colocar todos os nomes em maiúsculas
            
            // EM
            IEnumerable<string> namesInUpperCase1 = orderedNames.Select(name => name.ToUpper());
            
            // TS
            IEnumerable<string> namesInUpperCase2 = from name in orderedNames
                                                    select name.ToUpper();

            foreach(string name in orderedNames)
            {
                Console.WriteLine($"{name}");
            }
            
        }
    }
}
