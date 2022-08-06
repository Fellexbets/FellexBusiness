using System;

namespace Upskill.Teste
{
    class Program
    {
        // Cao[] matilha = new Cao[10];
        // matilha[0] = new Cao("Tobias", 20, 2, "rafeiro");
        // matilha[1] = new Cao("Baco", 10, 2, "Chow Chow");
        // foreach(Cao cao in matilha)
        // {
        //      System.Console.WriteLine(cao.Nome);
        // }

        // Gato[] gataria = new Gato[4]; 
        // gataria[0] = new Gato(true, "Lima", "Comum", 6);
        // gataria[1] = new Gato(true, "Mia", "Siamês", 9);
        // gataria[2] = new Gato(true, "Artur", "Comum", 3);
        // gataria[3] = new Gato(true, "Maui", "Comum", 0);
        // foreach(Gato g in gataria)
        // {
        //     Console.WriteLine(g.Nome);
        // }

        static void Main(string[] args)
        {
            Peixe[] cardume = new Peixe[2];
            cardume[0] = new Peixe(1, 60, 5000, "bacalhau");
            cardume[1] = new Peixe(1, 60, 5000, "bacalhau");

            foreach(Peixe p in cardume)
            {
                Console.WriteLine(p.Identificacao());
            }
        }
    }
}
