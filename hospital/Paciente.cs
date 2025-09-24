using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital
{
    class Paciente
    {
        
        public string nome;
        public int idade;
        public bool preferencial;

        public string Exibir()
        {
            if (preferencial)
            {
                return nome + " - " + idade + " anos - Preferencial";
            }
            else
            {
                return nome + " - " + idade + " anos - Comum";
            }
        }



    }
}

