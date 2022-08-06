using System;

namespace Animais
{
    public class Cao : Mamifero, IEquatable<Cao>
    {
        public Cao(string nome, string raca, int peso, int idade) : base(nome, raca, peso, idade)
        {
        }

        private string Ladrar() 
        {
            return "Ão ão";    
        }

        public override string Vocalizar()
        {
            return Ladrar();
        }

        public string Babar() 
        {
            return "Saliva...";
        }

        // System.Object
        public override int GetHashCode() => HashCode.Combine(Nome, raca);

        // System.Object
        public override bool Equals(object obj) => Equals(obj as Cao);

        // IEquatable
        public bool Equals(Cao other) => this.Nome == other.Nome && this.raca == other.raca;

        public static bool operator ==(Cao cao, Cao outroCao) => cao.Equals(outroCao);

        public static bool operator !=(Cao cao, Cao outroCao) => !cao.Equals(outroCao); // !(cao == outroCao);
    }
}