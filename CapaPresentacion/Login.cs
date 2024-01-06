using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            List<Usuario> TEST = new CN_Usuario().Listar();

            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.documento == textDocumento.Text && u.clave == textClave.Text).FirstOrDefault();

            if(oUsuario != null)
            {
                Inicio form = new Inicio();

                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;

            } else
            {
                MessageBox.Show("No se encontró el usuario","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            
        }

        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            textDocumento.Text = "";
            textClave.Text = "";

            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
