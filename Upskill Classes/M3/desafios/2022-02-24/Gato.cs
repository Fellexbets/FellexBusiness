namespace Upskill.Teste
{
    class Gato
    {
        private bool pelo;
        public string Nome { get; private set; }
        private string raca;
        private int idade;

        public Gato(bool pelo, string nome, string raca, int idade)
        {
            this.pelo = pelo;
            this.Nome = nome;
            this.raca = raca;
            this.idade = idade;
        }

        public string Identificacao()
        {
            return Nome + " " + idade;
        }
    }
}