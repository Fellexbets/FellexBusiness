namespace Animais
{
    public class Gato : Mamifero
    {
        public Gato(string nome, string raca, int peso, int idade) : base(nome, raca, peso, idade)
        {
        }
        private string Miar()
        {
            return "miau";
        }

        public override string Vocalizar()
        {
            return Miar();
        }
    }
}