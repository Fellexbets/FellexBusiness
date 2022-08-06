using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animais
{
    public abstract class Mamifero : Animal
    {
        protected string raca;

        private static int nPatas = 4;
        public static int NumPatas()
        {   
            return nPatas;
        }

        public Mamifero(string nome, string raca, int peso, int idade) : base(nome, peso, idade)
        {
            this.raca = raca;
        }

        public override string ToString()
        {
            return $"{Nome}, {raca}, {peso} Kg, {idade} anos";
        }
    }
}
