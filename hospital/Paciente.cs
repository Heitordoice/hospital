using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital
{
    class Paciente
    {
        public string Nome;
        public int Idade;
        public bool Preferencial;

        public string Exibir()
        {
            if (Preferencial)
            {
                return Nome + " - " + Idade + " anos - Preferencial";
            }
            else
            {
                return Nome + " - " + Idade + " anos - Comum";
            }
        }
    }
}


