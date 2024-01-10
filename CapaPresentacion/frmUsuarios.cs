using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;

using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }



        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            comboEstado.Items.Add(new OpcionCombo() { valor = 1, texto = "Activo" });
            comboEstado.Items.Add(new OpcionCombo() { valor = 2, texto = "No activo" });
            comboEstado.DisplayMember = "texto";
            comboEstado.ValueMember = "valor";
            comboEstado.SelectedIndex = 0;

            List<Rol> listaRol = new CN_Rol().Listar();

            foreach(Rol item in listaRol)
            {
                comboRol.Items.Add(new OpcionCombo() { valor = item.idRol, texto = item.descripcion });
            }
            comboRol.DisplayMember = "texto";
            comboRol.ValueMember = "valor";
            comboRol.SelectedIndex = 0;

            foreach(DataGridViewColumn columna in datagridviewData.Columns)
            {
                if(columna.Visible && columna.Name != "btnSeleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, texto = columna.HeaderText });
                }
            }

            comboBusqueda.DisplayMember = "texto";
            comboBusqueda.ValueMember = "valor";
            comboBusqueda.SelectedIndex = 0;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            datagridviewData.Rows.Add(new object[] {"",textId.Text,textDocumento.Text,textNombreCompleto.Text,textCorreo.Text,textClave.Text,
                ((OpcionCombo)comboRol.SelectedItem).valor.ToString(),
                ((OpcionCombo)comboRol.SelectedItem).texto.ToString(),
                ((OpcionCombo)comboEstado.SelectedItem).valor.ToString(),
                ((OpcionCombo)comboEstado.SelectedItem).texto.ToString()
            });

            Limpiar();
        }

        private void Limpiar()
        {
            textId.Text = "0";
            textDocumento.Text = "";
            textNombreCompleto.Text = "";
            textCorreo.Text = "";
            textClave.Text = "";
            textConfirmarClave.Text = "";
            comboRol.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
