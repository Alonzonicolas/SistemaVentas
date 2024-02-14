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

namespace CapaPresentacion.Modales
{
    public partial class mdProducto : Form
    {
        public Producto _Producto {  get; set; }

        public mdProducto()
        {
            InitializeComponent();
        }

        private void mdProducto_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in datagridviewData.Columns)
            {
                if (columna.Visible)
                {
                    comboBusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, texto = columna.HeaderText });
                }
            }

            comboBusqueda.DisplayMember = "texto";
            comboBusqueda.ValueMember = "valor";
            comboBusqueda.SelectedIndex = 0;


            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                datagridviewData.Rows.Add(new object[] {item.idProducto,item.codigo,item.nombre,
                    item.oCategoria.descripcion,
                    item.stock,
                    item.precioCompra,
                    item.precioVenta,
            });

            }
        }

        private void datagridviewData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iColum = e.ColumnIndex;

            if(iRow >= 0 && iColum > 0)
            {
                _Producto = new Producto()
                {
                    idProducto = Convert.ToInt32(datagridviewData.Rows[iRow].Cells["Id"].Value.ToString()),
                    codigo = datagridviewData.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    nombre = datagridviewData.Rows[iRow].Cells["Nombre"].Value.ToString(),
                    stock = Convert.ToInt32(datagridviewData.Rows[iRow].Cells["Stock"].Value.ToString()),
                    precioCompra = Convert.ToDecimal(datagridviewData.Rows[iRow].Cells["PrecioCompra"].Value.ToString()),
                    precioVenta = Convert.ToDecimal(datagridviewData.Rows[iRow].Cells["PrecioVenta"].Value.ToString()),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
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
    }
}
