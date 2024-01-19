using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            comboEstado.Items.Add(new OpcionCombo() { valor = 1, texto = "Activo" });
            comboEstado.Items.Add(new OpcionCombo() { valor = 2, texto = "No Activo" });
            comboEstado.DisplayMember = "texto";
            comboEstado.ValueMember = "valor";
            comboEstado.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in datagridviewData.Columns)
            {
                if (columna.Visible && columna.Name != "btnSeleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, texto = columna.HeaderText });
                }
            }

            comboBusqueda.DisplayMember = "texto";
            comboBusqueda.ValueMember = "valor";
            comboBusqueda.SelectedIndex = 0;


            //MOSTRAR TODOS LOS USUARIOS
            List<Categoria> lista = new CN_Categoria().Listar();

            foreach (Categoria item in lista)
            {
                datagridviewData.Rows.Add(new object[] {"",item.idCategoria,
                    item.descripcion,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "No Activo"
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Categoria obj = new Categoria()
            {
                idCategoria = Convert.ToInt32(textId.Text),
                descripcion = textDescripcion.Text,
                estado = Convert.ToInt32(((OpcionCombo)comboEstado.SelectedItem).valor) == 1 ? true : false
            };


            if (obj.idCategoria == 0)
            {
                int idGenerado = new CN_Categoria().Registrar(obj, out mensaje);

                if (idGenerado != 0)
                {
                    datagridviewData.Rows.Add(new object[] {"",idGenerado,textDescripcion.Text,
                    ((OpcionCombo)comboEstado.SelectedItem).valor.ToString(),
                    ((OpcionCombo)comboEstado.SelectedItem).texto.ToString()
                });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Categoria().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = datagridviewData.Rows[Convert.ToInt32(textIndice.Text)];
                    row.Cells["Id"].Value = textId.Text;
                    row.Cells["Descripcion"].Value = textDescripcion.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)comboEstado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)comboEstado.SelectedItem).texto.ToString();

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void Limpiar()
        {
            textIndice.Text = "-1";
            textId.Text = "0";
            textDescripcion.Text = "";
            comboEstado.SelectedIndex = 0;

            textDescripcion.Select();
        }

        private void datagridviewData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
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

                if (indice >= 0)
                {
                    textIndice.Text = indice.ToString();
                    textId.Text = datagridviewData.Rows[indice].Cells["Id"].Value.ToString();
                    textDescripcion.Text = datagridviewData.Rows[indice].Cells["Descripcion"].Value.ToString();


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

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(textId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el la categoría?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Categoria obj = new Categoria()
                    {
                        idCategoria = Convert.ToInt32(textId.Text)
                    };


                    bool respuesta = new CN_Categoria().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        datagridviewData.Rows.RemoveAt(Convert.ToInt32(textIndice.Text));
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)comboBusqueda.SelectedItem).valor.ToString();

            if (datagridviewData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in datagridviewData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(textBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            textBusqueda.Text = "";
            foreach (DataGridViewRow row in datagridviewData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
