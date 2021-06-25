using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace frmCinema
{
    class Conexao
    {
        public SqlConnection conexao = new SqlConnection();

        public void conectar()
        {
            string diretorio = Directory.GetCurrentDirectory();
            diretorio = diretorio.Remove(diretorio.Count() - 10);
            conexao.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + diretorio + "\\banco.mdf";
            conexao.Open();
        }
        public void desconectar()
        {
            conexao.Close();
        }
    }
}
