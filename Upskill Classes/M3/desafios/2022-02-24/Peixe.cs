namespace Upskill.Teste
{
    class Peixe
    {
        int tanque;
        int comprimento;
        int peso;
        public string Especie { get; private set; }

        public Peixe(int tanque, int comprimento, int peso, string especie)
        {
            this.tanque = tanque;
            this.comprimento = comprimento;
            this.peso = peso;
            this.Especie = especie;
        }

        public string Identificacao()
        {
            return Especie + " " + peso + " " + tanque;
        }
    }
}