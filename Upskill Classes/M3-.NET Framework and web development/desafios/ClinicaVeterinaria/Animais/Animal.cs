using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animais
{
    public abstract class Animal : IVocalizar
    {
        public string Nome { get; private set; }
        protected int peso;
        protected int idade;

        public Animal(string nome, int peso, int idade) {
            Nome = nome;
            this.peso = peso;
            this.idade = idade;
        }

        public override string ToString() 
        {
            return $"{Nome}, {peso}Kg, {idade} anos";
        }

        public abstract string Vocalizar();
    }
}
