namespace Upskill.Teste
{
    class Cao
    {
        public string Nome { get; private set; }

        public double Peso;
        private int idade;
        private string raca;
        private bool chip;
        private int numeroChip;
        private int comprimento;
        public static int nPatas = 4;

        public Cao(string nome, double peso, int idade, string raca){
            this.Nome = nome;
            this.Peso = peso;
            this.idade = idade;
            this.raca = raca;
        }

        public static string IdentificacaoClasse(){
            return "Cao " + nPatas;
        }

        public string Identificacao(){
            return $"{Nome}, {Peso}Kg, {idade}anos, {raca}, {comprimento}cm";
        }

        public void AfectarChip(int numeroChip){
            chip = true;
            this.numeroChip = numeroChip;
        }

        
    }
}