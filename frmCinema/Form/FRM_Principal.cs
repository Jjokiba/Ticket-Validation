using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace frmCinema
{
    public partial class frmCinema : Form
    {
        public frmCinema()
        {
            InitializeComponent();
            telaInicial();
            txtTicket.MaxLength = 2;
        }
        public void carregarAssento()
        {
            if (Validacao.verificarOcupado(int.Parse(btn1.Text)) == true)
            { btn1.BackgroundImage = Properties.Resources.TICKECTGET;}
            else { btn1.BackgroundImage = Properties.Resources.TICKECTNEW;}
            if (Validacao.verificarOcupado(int.Parse(btn2.Text)) == true)
            { btn2.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn2.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn3.Text)) == true)
            { btn3.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn3.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn4.Text)) == true)
            { btn4.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn4.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn5.Text)) == true)
            { btn5.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn5.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn6.Text)) == true)
            { btn6.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn6.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn7.Text)) == true)
            { btn7.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn7.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn8.Text)) == true)
            { btn8.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn8.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn9.Text)) == true)
            { btn9.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn9.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn10.Text)) == true)
            { btn10.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn10.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn11.Text)) == true)
            { btn11.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn11.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn12.Text)) == true)
            { btn12.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn12.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn13.Text)) == true)
            { btn13.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn13.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn14.Text)) == true)
            { btn14.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn14.BackgroundImage = Properties.Resources.TICKECTNEW; }
            if (Validacao.verificarOcupado(int.Parse(btn15.Text)) == true)
            { btn15.BackgroundImage = Properties.Resources.TICKECTGET; }
            else { btn15.BackgroundImage = Properties.Resources.TICKECTNEW; }
        }

        private void TelaResult()
        {
            txtResultado.Visible = false;
            txtTicket.Enabled = false;
            btnDesvalidar.Enabled = true;
            btnVoltar.Enabled = true;
            panel2.Enabled = true;
            btnValidar.Enabled = false;
            btnSair.Enabled = false;
            pesquisaValid();
            pesquisaNaoValid();
            
        }

        private void verificar(object sender, EventArgs e)
        {

            if (Validacao.verificarOcupado(int.Parse((sender as Button).Text)))
            {
                TelaResult();
                btnDesvalidar.Enabled = true;
                buscaDataValidada(int.Parse((sender as Button).Text));
            }
            else
            {
                telaInicial();
                btnVoltar.Enabled = true;
                btnSair.Enabled = false;
                txtResultado.Text = "";
            }
            txtTicket.Text = "";
            
            txtTicket.Text = (sender as Button).Text;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtTicket.Text == "")
            {
                MessageBox.Show("Informe um ticket");
                txtTicket.Focus();
            }
            else
            {
                string query = "SELECT * FROM TICKET WHERE ID=@ID";
                Conexao conexao = new Conexao();
                conexao.conectar();
                SqlCommand cmd = new SqlCommand(query, conexao.conexao);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtTicket.Text));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Validacao.validar(dr["id"].ToString(), dr["data"].ToString());
                    if (Validacao.validarTicket(Convert.ToInt32(txtTicket.Text)) == true)
                    {
                        btnDesvalidar.Enabled = true;
                        btnVoltar.Enabled = true;
                        txtTicket.Enabled = false;
                        txtResultado.Visible = true;
                        exibirResumo();
                        if (dr["data"].ToString() == "")
                        {
                            DateTime dataT = DateTime.Now;
                            string dataFormato = dataT.ToString("f");
                            txtResultado.Text = dataFormato.ToString();
                            MessageBox.Show(Validacao.getTicket());
                            carregarAssento();
                        }
                        else
                        {
                            txtResultado.Text = dr["data"].ToString();
                            MessageBox.Show(Validacao.getTicketJaValidado());
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERRO INESPERADO");
                    }
                }
                else
                {
                    MessageBox.Show("ERRO: Ticket nao foi encontrato");
                    txtTicket.Text = "";
                    txtTicket.Focus();
                }
                dr.Close();
                conexao.desconectar();
                carregarAssento();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            telaInicial();
        }

        private void buscaDataValidada(int ID)
        {
            Conexao con = new Conexao();
            con.conectar();
            string query = "SELECT DATA FROM TICKET WHERE ID = @ID";
            SqlCommand sSql = new SqlCommand(query, con.conexao);
            sSql.Parameters.AddWithValue("@ID", ID); 
            SqlDataReader dr = sSql.ExecuteReader();
            txtResultado.Visible = true;
            while (dr.Read())
            {
                txtResultado.Text = dr["DATA"].ToString();
            }
            
            con.desconectar();
        }

        private void pesquisaValid()
        {

            Conexao con = new Conexao();
            con.conectar();
            string query = "SELECT COALESCE(COUNT(ID), 0) AS NUM_VALID FROM TICKET WHERE DATA IS NULL";
            SqlCommand sSql = new SqlCommand(query, con.conexao);
            SqlDataReader dr = sSql.ExecuteReader();
            while(dr.Read())
            {
                lblNumValidado.Text = dr["NUM_VALID"].ToString();
            }
            con.desconectar();
        }

        private void pesquisaNaoValid()
        {

            Conexao con = new Conexao();
            con.conectar();
            string query = "SELECT COALESCE(COUNT(ID), 0) AS NUM_NAO_VALID FROM TICKET WHERE DATA IS NOT NULL";
            SqlCommand sSql = new SqlCommand(query, con.conexao);
            SqlDataReader dr = sSql.ExecuteReader();
            while (dr.Read())
            {
                lblNumNaoValid.Text = dr["NUM_NAO_VALID"].ToString();
            }
            con.desconectar();
        }

        private void btnDesvalidar_Click(object sender, EventArgs e)
        {
            if (txtTicket.Text == "")
            {
                MessageBox.Show("Informe um ticket");
                txtTicket.Focus();
            }
            else
            {
                string query = "SELECT * FROM TICKET WHERE ID=@ID";
                Conexao conexao = new Conexao();
                conexao.conectar();
                SqlCommand cmd = new SqlCommand(query, conexao.conexao);
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(txtTicket.Text));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Validacao.desvalidarTicket(Convert.ToInt32(dr["id"]));
                    if (Validacao.desvalidarTicket(Convert.ToInt32(txtTicket.Text)) == true)
                    {
                        btnDesvalidar.Enabled = true;
                        btnVoltar.Enabled = true;
                        txtResultado.Visible = false;
                        exibirResumo();
                        if (dr["data"].ToString() == "")
                        {
                            DateTime dataT = DateTime.Now;
                            string dataFormato = dataT.ToString("f");
                            txtResultado.Text = dataFormato.ToString();
                            MessageBox.Show(Validacao.getTicketJaDesvalidado());
                            carregarAssento();
                            
                        }
                        else
                        {
                            txtResultado.Text = dr["data"].ToString();
                            MessageBox.Show(Validacao.getTicketDesvalidado());
                            telaInicial();
                            txtTicket.Text = "";
                            txtResultado.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERRO INESPERADO");
                    }
                }
                else
                {
                    MessageBox.Show("ERRO: Ticket nao foi encontrato");
                    txtTicket.Text = "";
                    txtTicket.Focus();
                }
                dr.Close();
                conexao.desconectar();
                carregarAssento();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Deseja Sair?", "Aviso", MessageBoxButtons.YesNo);
            if (d.ToString() == "No")
            {
                Application.Exit();
            }
            this.Close();
        }
        public void telaInicial()
        {
            btnDesvalidar.Enabled = false;
            btnVoltar.Enabled = false;
            txtResultado.Visible = false;
            txtResultado.Enabled = false;
            txtTicket.Enabled = true;
            txtTicket.Text = "";
            btnValidar.Enabled = true;
            panel2.Enabled = true;
            btnSair.Enabled = true;
            exibirResumo();
            carregarAssento();
            txtTicket.Focus();
            txtTicket.Enabled = false;
            pesquisaValid();
            pesquisaNaoValid();
        }
        public void exibirResumo()
        {
        }

        private void validarInteiro(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) ||
                char.IsWhiteSpace(e.KeyChar) ||
                e.KeyChar == (char)(Keys.Back))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        // Funções de minimizar e mover o form quando clicado abaixo https://stackoverflow.com/questions/1592876/make-a-borderless-form-movable
        private bool mouseDown;
        private Point lastLocation;

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        
        private void frmCinema_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void moveFormCasoMouseAtivo(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void frmCinema_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

    }
}
