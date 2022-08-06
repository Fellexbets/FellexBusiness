using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animais
{
    public class Alexa : IVocalizar
    {
        public string Vocalizar()
        {
            return "Hey Alexa!";
        }
    }
}
