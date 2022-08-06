using System;

namespace Conversao
{
    class Program
    {
        static string Input(){
            Console.WriteLine("insira a temperatura em C");
            string tempCTxt = Console.ReadLine();
            return tempCTxt;
        }
        static void Output(string msg){
            Console.WriteLine(msg);
        }

        static string ConvertCtoF(string tempCTxt){
            bool isNumber = Int32.TryParse(tempCTxt, out int tempC);
            if(isNumber){
                double tempF = tempC * 1.8 + 32;
                return String.Format("A temperatura é {0}", tempF);
            }
            return "Só funciona com números";
        }

        static void Main(string[] args)
        {
            string tempCTxt = Input();
            string msg = ConvertCtoF(tempCTxt);
            Output(msg);
        }
    }
}
