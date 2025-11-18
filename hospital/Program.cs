using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fila fila = new Fila();
            string opcao = "";

            while (opcao != "q")
            {
                Console.WriteLine("\n   MENU   ");
                Console.WriteLine("1 - Cadastrar paciente");
                Console.WriteLine("2 - Listar pacientes");
                Console.WriteLine("3 - Atender paciente");
                Console.WriteLine("4 - Alterar dados");
                Console.WriteLine("q - Sair");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    fila.CadastrarPaciente();
                }
                else if (opcao == "2")
                {
                    fila.ListarFila();
                }
                else if (opcao == "3")
                {
                    fila.AtenderPaciente();
                }
                else if (opcao == "4")
                {
                    fila.AlterarPaciente();
                }
                else if (opcao != "q")
                {
                    Console.WriteLine("Opção inválida!");
                }
            }

            Console.WriteLine("Saindo do sistema...");
        }
    }
    }
