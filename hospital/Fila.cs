using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital
{
    class Fila
    {
        private Paciente[] pacientes;
        private int total;
        private const int max = 15;

        public Fila()
        {
            pacientes = new Paciente[max];
            total = 0;
        }
        public void Cadastrar(Paciente pc)
        {
            if (total >= max)
            {
                Console.WriteLine("Fila cheia!");
            }
            else
            {
                if (pc.preferencial)
                {
                    for (int i = total; i > 0; i--)
                    {
                        pacientes[i] = pacientes[i - 1];
                    }
                    pacientes[0] = pc;
                }
                else
                {
                    pacientes[total] = pc;
                }

                total++;
                Console.WriteLine("Paciente cadastrado!\n");
            }
        }

        public void Listar()
        {
            if (total == 0)
            {
                Console.WriteLine("Fila vazia.\n");
            }
            else
            {
                for (int i = 0; i < total; i++)
                {
                    Console.WriteLine((i + 1) + ". " + pacientes[i].Exibir());
                }
            }
        }

        public void Atender()
        {
            if (total == 0)
            {
                Console.WriteLine("Nenhum paciente na fila.\n");
            }
            else
            {
                Paciente atendido = pacientes[0];
                Console.WriteLine("Atendendo: " + atendido.nome);

                for (int i = 0; i < total - 1; i++)
                {
                    pacientes[i] = pacientes[i + 1];
                }

                pacientes[total - 1] = null;
                total--;
            }
        }
        public void Alterar(int pos, string novoNome, int novaIdade, bool altIdade, bool novoPreferencial, bool altPreferencial)
        {
            if (pos < 0 || pos >= total)
            {
                Console.WriteLine("Número inválido!\n");
            }
            else
            {
                if (novoNome != "")
                {
                    pacientes[pos].nome = novoNome;
                }

                if (altIdade)
                {
                    pacientes[pos].idade = novaIdade;
                }

                if (altPreferencial)
                {
                    pacientes[pos].preferencial = novoPreferencial;
                }

                Console.WriteLine("Dados alterados!");
            }
        }
        public int Total()
        {
            return total;
        }
    }

}

