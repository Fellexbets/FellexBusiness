namespace Animais
{
    public class Peixe : Animal
    {
        public string Especie { get; private set; } 

        public Peixe(string nome, string especie, int peso, int idade) : base(nome, peso, idade)
        {
            Especie = especie;
        }

        public override string ToString()
        {
            return $"{Nome}, {Especie}, {peso} g, {idade} anos";
        }

        private string Peixar()
        {
            return "Glu glu";
        }
        public override string Vocalizar()
        {
            return Peixar();
        }
    }
}