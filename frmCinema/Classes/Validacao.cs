using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace frmCinema
{
    class Validacao
    {
        static string id;
        static string data;

        public static void validar(string id1, string data1)
        {
            id = id1;
            data = data1;
        }

        public static Boolean validarTicket(int id)
        {
            DateTime dataT = DateTime.Now;
            string dataFormato = dataT.ToString("d");

            if (Convert.ToString(id) != "")
            {
                Conexao conexao = new Conexao();
                conexao.conectar();
                string query = "UPDATE TICKET SET DATA=@DATA WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, conexao.conexao);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@DATA", Convert.ToDateTime(dataFormato));
                cmd.ExecuteReader();
                conexao.desconectar();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Boolean desvalidarTicket(int id2)
        {
            DateTime dataT = DateTime.Now;
            string dataFormato = dataT.ToString("d");
            id = id2.ToString();
            if (Convert.ToString(id2) != "")
            {
                Conexao conexao = new Conexao();
                conexao.conectar();
                string query = "UPDATE TICKET SET DATA=NULL WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, conexao.conexao);
                cmd.Parameters.AddWithValue("@ID", id2);
                cmd.ExecuteReader();
                conexao.desconectar();
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public static int qtdeTicket()
        {
            int qtde = 0;
            Conexao cnx = new Conexao();
            string query = "SELECT COUNT(ID) AS QTDE FROM TICKET";
            SqlCommand cmd = new SqlCommand(query, cnx.conexao);
            cnx.conectar();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                qtde = Convert.ToInt32(dr["QTDE"].ToString());
            }
            cnx.desconectar();
            return qtde;
        }

        public static Boolean verificarOcupado(int id)
        {
            Conexao conexao = new Conexao();
            conexao.conectar();
            string query = "SELECT DATA FROM TICKET WHERE ID=@ID";
            SqlCommand cmd = new SqlCommand(query, conexao.conexao);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            SqlDataReader dr = cmd.ExecuteReader();
            bool foi = false;
            if (dr.Read())
            {
                if (dr["DATA"].ToString() == "")
                {
                    foi = false;
                }
                else
                {
                    foi = true;
                }
            }
            else
            {
                foi = false;
            }
            conexao.desconectar();
            return foi;
            
        }

        public static int qtdeTicketValidados()
        {
            return 0;
        }


        public static String getTicket()
        {
            return "TICKET ==>  " + id + "\nV A L I D A D O";
        }

        public static String getTicketJaValidado()
        {
            return "TICKET ==>  " + id + "\nJ Á    V A L I D A D O";
        }

        public static String getTicketJaDesvalidado()
        {
            return "TICKET ==> " + id + "\nJ Á  D E S V A L I D A D O";
        }

        public static String getTicketDesvalidado()
        {
            return "TICKET ==>  " + id + "\nD E S V A L I D A D O";
        }

    }
}
