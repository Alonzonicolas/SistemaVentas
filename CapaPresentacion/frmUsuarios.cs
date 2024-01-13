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
            comboEstado.Items.Add(new OpcionCombo() { valor = 2, texto = "No Activo" });
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


            //MOSTRAR TODOS LOS USUARIOS
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                datagridviewData.Rows.Add(new object[] {"",item.idUsuario,item.documento,item.nombreCompleto,item.correo,item.clave,
                    item.oRol.idRol,
                    item.oRol.descripcion,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "No Activo"
            });

            }

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
            textIndice.Text = "-1";
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

        private void datagridviewData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

            if(e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var ancho = Properties.Resources.check20.Width;
                var alto = Properties.Resources.delete20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - ancho) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - alto) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, ancho, alto));
                e.Handled = true;
            }

        }

        private void datagridviewData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (datagridviewData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    textIndice.Text = indice.ToString();
                    textId.Text = datagridviewData.Rows[indice].Cells["Id"].Value.ToString();
                    textDocumento.Text = datagridviewData.Rows[indice].Cells["Documento"].Value.ToString();
                    textNombreCompleto.Text = datagridviewData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    textCorreo.Text = datagridviewData.Rows[indice].Cells["Correo"].Value.ToString();
                    textClave.Text = datagridviewData.Rows[indice].Cells["Clave"].Value.ToString();
                    textConfirmarClave.Text = datagridviewData.Rows[indice].Cells["Clave"].Value.ToString();


                    foreach(OpcionCombo oc in comboRol.Items)
                    {
                        if( Convert.ToInt32(oc.valor) == Convert.ToInt32(datagridviewData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = comboRol.Items.IndexOf(oc);
                            comboRol.SelectedIndex = indiceCombo;
                            break;
                        }
                    }


                    foreach (OpcionCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(datagridviewData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo;
                            break;
                        }
                    }
                }
            }

        }

    }
}
