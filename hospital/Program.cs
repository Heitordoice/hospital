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
          {
        
            {
               Fila fila = new Fila();
               string opcao = "";

                    while (opcao != "q")
                    {
                        Console.WriteLine("   MENU   ");
                        Console.WriteLine("1 - Cadastrar paciente\n"+ "2 - Listar pacientes\n"+ "3 - Atender paciente\n" + "4 - Alterar dados\n"+ "q - Sair");
                        Console.Write("Escolha: ");
                        opcao = Console.ReadLine();

                        if (opcao == "1")
                        {
                            Paciente p = new Paciente();
                            Console.Write("Nome: ");
                            p.nome = Console.ReadLine();
                            Console.Write("Idade: ");
                            p.idade = int.Parse(Console.ReadLine());
                            Console.Write("Preferencial (s/n): ");
                            string pref = Console.ReadLine();

                            if (pref == "s")
                            {
                                p.preferencial = true;
                            }
                            else
                            {
                                p.preferencial = false;
                            }

                            fila.Cadastrar(p);
                        }
                        else if (opcao == "2")
                        {
                            fila.Listar();
                        }
                        else if (opcao == "3")
                        {
                            fila.Atender();
                        }
                        else if (opcao == "4")
                        {
                            fila.Listar();
                            if (fila.Total() > 0)
                            {
                                Console.Write("Número do paciente: ");
                                int pos = int.Parse(Console.ReadLine()) - 1;

                                Console.Write("Novo nome (enter p/ manter): ");
                                string nome = Console.ReadLine();

                                Console.Write("Nova idade (enter p/ manter): ");
                                string idadeStr = Console.ReadLine();
                                int idade = 0;
                                bool alterarIdade = false;
                                if (idadeStr != "")
                                {
                                    idade = int.Parse(idadeStr);
                                    alterarIdade = true;
                                }

                                Console.Write("Preferencial (s/n ou enter p/ manter): ");
                                string pref = Console.ReadLine();
                                bool novoPref = false;
                                bool alterarPref = false;
                                if (pref == "s")
                                {
                                    novoPref = true;
                                    alterarPref = true;
                                }
                                else if (pref == "n")
                                {
                                    novoPref = false;
                                    alterarPref = true;
                                }

                                fila.Alterar(pos, nome, idade, alterarIdade, novoPref, alterarPref);
                            }
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
    }
    }

