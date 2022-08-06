using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "André", "João",
                               "Rui", "Marta", "Mariana", "Inês", "Igor",
                               "Alexandre", "Fernando", "Xavier",
                               "Rafael", "Ivo", "Luiz"};
            // Desafio:        
            // - Seleccionar nomes com letra 'a', ordenar por ordem
            //      crescente do número de caracteres e colocar os nomes em maiúsculas
            IEnumerable<string> namesWithA = names.Where(name => name.Contains("a") || name.Contains("A") || name.Contains("ã"))
                                                  .OrderBy(name => name.Length)
                                                  .Select(name => name.ToUpper());
            foreach(string name in namesWithA) 
            { 
                Console.WriteLine(name);
            }
        }
    }
}
