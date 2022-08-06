using System;
using System.Collections.Generic;
using Animais;

namespace AppConsola
{
    public class Program
    {
        public static void TesteVocalizar() {
            IVocalizar[] animaisQueVocalizam = new IVocalizar[9];
            animaisQueVocalizam[0] = new Cao("Tobias", "Rafeiro", 20, 2);
            animaisQueVocalizam[1] = new Cao("Baco", "Chow Chow", 10, 2);
            animaisQueVocalizam[2] = new Gato("Lima", "Comum", 3, 6);
            animaisQueVocalizam[3] = new Gato("Mia", "Siamês", 3, 9);
            animaisQueVocalizam[4] = new Gato("Artur", "Comum", 3, 3);
            animaisQueVocalizam[5] = new Gato("Maui", "Comum", 3, 0);
            animaisQueVocalizam[6] = new Peixe("Nemo", "Peixe-palhaço", 100, 2);
            animaisQueVocalizam[7] = new Peixe("Dory", "Cirurgião-patela", 100, 1);
            animaisQueVocalizam[8] = new Alexa();

            foreach (IVocalizar vocalizadores in animaisQueVocalizam)
            {
                Console.WriteLine(vocalizadores.Vocalizar());
            }
        }

        public static void Main(string[] args)
        {
            List<Cao> matilha = new List<Cao>();
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            matilha.Add(new Cao("Tobias", "Rafeiro", 20, 2));
            foreach(Cao c in matilha)
            { 
                Console.WriteLine(c);
            }
        }
    }
}
