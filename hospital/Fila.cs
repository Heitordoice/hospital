using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace hospital
{
    class Fila
    {
        private string conexaoString = "server=localhost;port=3307;user=root;password='';database=clinica;";

        Paciente[] paciente = new Paciente[100];
        int quantidade = 0;


        public void CadastrarPaciente()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Idade: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("Preferencial (s/n): ");
            bool pref = Console.ReadLine().Trim().ToLower() == "s";


            paciente[quantidade] = new Paciente();
            paciente[quantidade].Nome = nome;
            paciente[quantidade].Idade = idade;
            paciente[quantidade].Preferencial = pref;
            quantidade++;

      
            try
            {
                MySqlConnection conexao = new MySqlConnection(conexaoString);
                conexao.Open();

                string sql = "INSERT INTO paciente (nome, idade, preferencial) VALUES (@n, @i, @p)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@n", nome);
                cmd.Parameters.AddWithValue("@i", idade);
                cmd.Parameters.AddWithValue("@p", pref);

                cmd.ExecuteNonQuery();
                conexao.Close();

                Console.WriteLine("\nPaciente cadastrado!\n");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
            }
        }

    
        public void ListarFila()
        {
            Console.WriteLine("\n--- FILA ---");

            int pos = 1;


            for (int i = 0; i < quantidade; i++)
            {
                if (paciente[i].Preferencial)
                {
                    Console.WriteLine($"{pos}. {paciente[i].Nome} - Idade: {paciente[i].Idade} - Preferencial");
                    pos++;
                }
            }

        
            for (int i = 0; i < quantidade; i++)
            {
                if (!paciente[i].Preferencial)
                {
                    Console.WriteLine($"{pos}. {paciente[i].Nome} - Idade: {paciente[i].Idade} - Comum");
                    pos++;
                }
            }

 
            Console.WriteLine("\n--- FILA ---");

            try
            {
                MySqlConnection conexao = new MySqlConnection(conexaoString);
                conexao.Open();

                string sql = "SELECT nome, idade, preferencial FROM paciente ORDER BY preferencial DESC, id ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nome = reader.GetString("nome");
                    int idade = Convert.ToInt32(reader["idade"]);
                    bool pref = Convert.ToBoolean(reader["preferencial"]);

                    Console.WriteLine($"{nome} - Idade: {idade} - {(pref ? "Preferencial" : "Comum")}");
                }

                reader.Close();
                conexao.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao listar: " + ex.Message);
            }
        }


        public void AtenderPaciente()
        {
    
            int index = -1;

    
            for (int i = 0; i < quantidade; i++)
            {
                if (paciente[i].Preferencial)
                {
                    index = i;
                    break;
                }
            }

    
            if (index == -1 && quantidade > 0)
                index = 0;

            if (index != -1)
            {
                Console.WriteLine($"\nPaciente atendido: {paciente[index].Nome}");


                for (int i = index; i < quantidade - 1; i++)
                {
                    paciente[i] = paciente[i + 1];
                }

                quantidade--;
            }
            else
            {
                Console.WriteLine("\nNenhum paciente.\n");
            }

     
            try
            {
                MySqlConnection conexao = new MySqlConnection(conexaoString);
                conexao.Open();

                string sqlBuscar = "SELECT id, nome FROM paciente ORDER BY preferencial DESC, id ASC LIMIT 1";
                MySqlCommand cmdBuscar = new MySqlCommand(sqlBuscar, conexao);

                MySqlDataReader reader = cmdBuscar.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("\nNenhum paciente no banco.\n");
                    reader.Close();
                    conexao.Close();
                    return;
                }

                int id = Convert.ToInt32(reader["id"]);
                string nomePaciente = reader.GetString("nome");

                reader.Close();

                string sqlDel = "DELETE FROM paciente WHERE id=@id";
                MySqlCommand cmdDel = new MySqlCommand(sqlDel, conexao);
                cmdDel.Parameters.AddWithValue("@id", id);
                cmdDel.ExecuteNonQuery();

                conexao.Close();

                Console.WriteLine($"Paciente atendido: {nomePaciente}\n");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao atender: " + ex.Message);
            }
        }


        public void AlterarPaciente()
        {
            Console.Write("Nome do paciente: ");
            string nomeBusca = Console.ReadLine();
            for (int i = 0; i < quantidade; i++)
            {
                if (paciente[i].Nome.ToLower() == nomeBusca.ToLower())
                {
                    Console.WriteLine("\nAlterando:");

                    Console.Write("Novo nome: ");
                    paciente[i].Nome = Console.ReadLine();

                    Console.Write("Nova idade: ");
                    paciente[i].Idade = int.Parse(Console.ReadLine());

                    Console.Write("Preferencial (s/n): ");
                    paciente[i].Preferencial = Console.ReadLine().Trim().ToLower() == "s";

                    Console.WriteLine("\nDados alterados!\n");
                    break;
                }
            }
            try
            {
                MySqlConnection conexao = new MySqlConnection(conexaoString);
                conexao.Open();

                string sqlBusca = "SELECT id FROM paciente WHERE LOWER(nome)=LOWER(@n)";
                MySqlCommand cmdBusca = new MySqlCommand(sqlBusca, conexao);
                cmdBusca.Parameters.AddWithValue("@n", nomeBusca);

                MySqlDataReader reader = cmdBusca.ExecuteReader();

                if (!reader.Read())
                {
                    Console.WriteLine("\nPaciente não encontrado.\n");
                    reader.Close();
                    conexao.Close();
                    return;
                }

                int id = Convert.ToInt32(reader["id"]);
                reader.Close();

                Console.WriteLine("\nAlterando:");

                Console.Write("Novo nome: ");
                string novoNome = Console.ReadLine();

                Console.Write("Nova idade: ");
                int novaIdade = int.Parse(Console.ReadLine());

                Console.Write("Preferencial (s/n): ");
                bool novoPref = Console.ReadLine().Trim().ToLower() == "s";

                string sqlAlt =
                    "UPDATE paciente SET nome=@n, idade=@i, preferencial=@p WHERE id=@id";

                MySqlCommand cmdAlt = new MySqlCommand(sqlAlt, conexao);

                cmdAlt.Parameters.AddWithValue("@n", novoNome);
                cmdAlt.Parameters.AddWithValue("@i", novaIdade);
                cmdAlt.Parameters.AddWithValue("@p", novoPref);
                cmdAlt.Parameters.AddWithValue("@id", id);

                cmdAlt.ExecuteNonQuery();
                conexao.Close();

                Console.WriteLine("\nDados atualizados!\n");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
            }
        }
    }
}
