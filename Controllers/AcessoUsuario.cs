using System;
using system.Collections.Generic;
using System.Linq;

namespace Controller
{
    public class AcessoUsuario
    {
        private MySqlConnection conexao; //Corrigir

        public AcessoUsuario(string connectionString)
        {
            conexao = new MySqlConnection(connectionString);
            conexao.Open();
        }

        public void FecharConexao()
        {
            conexao.Close();
        }
    }
}