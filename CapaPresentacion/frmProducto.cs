using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class frmProducto : Form
    {
        public frmProducto()
        {
            InitializeComponent();
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            comboEstado.Items.Add(new OpcionCombo() { valor = 1, texto = "Activo" });
            comboEstado.Items.Add(new OpcionCombo() { valor = 2, texto = "No Activo" });
            comboEstado.DisplayMember = "texto";
            comboEstado.ValueMember = "valor";
            comboEstado.SelectedIndex = 0;

            List<Categoria> listaCategoria = new CN_Categoria().Listar();

            foreach (Categoria item in listaCategoria)
            {
                comboCategoria.Items.Add(new OpcionCombo() { valor = item.idCategoria, texto = item.descripcion });
            }
            comboCategoria.DisplayMember = "texto";
            comboCategoria.ValueMember = "valor";
            comboCategoria.SelectedIndex = 0;

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
            List<Producto> lista = new CN_Producto().Listar();

            foreach (Producto item in lista)
            {
                datagridviewData.Rows.Add(new object[] {"",item.idProducto,item.codigo,item.nombre,item.descripcion,
                    item.oCategoria.idCategoria,
                    item.oCategoria.descripcion,
                    item.stock,
                    item.precioCompra,
                    item.precioVenta,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "No Activo"
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Producto obj = new Producto()
            {
                idProducto = Convert.ToInt32(textId.Text),
                codigo = textCodigo.Text,
                nombre = textNombre.Text,
                descripcion = textDescripcion.Text,
                oCategoria = new Categoria() { idCategoria = Convert.ToInt32(((OpcionCombo)comboCategoria.SelectedItem).valor) },
                estado = Convert.ToInt32(((OpcionCombo)comboEstado.SelectedItem).valor) == 1 ? true : false
            };


            if (obj.idProducto == 0)
            {
                int idGenerado = new CN_Producto().Registrar(obj, out mensaje);

                if (idGenerado != 0)
                {
                    datagridviewData.Rows.Add(new object[] {"",idGenerado,textCodigo.Text,textNombre.Text,textDescripcion.Text,
                    ((OpcionCombo)comboCategoria.SelectedItem).valor.ToString(),
                    ((OpcionCombo)comboCategoria.SelectedItem).texto.ToString(),
                    "0",
                    "0.00",
                    "0.00",
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
                bool resultado = new CN_Producto().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = datagridviewData.Rows[Convert.ToInt32(textIndice.Text)];
                    row.Cells["Id"].Value = textId.Text;
                    row.Cells["Codigo"].Value = textCodigo.Text;
                    row.Cells["Nombre"].Value = textNombre.Text;
                    row.Cells["Descripcion"].Value = textDescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)comboCategoria.SelectedItem).valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)comboCategoria.SelectedItem).texto.ToString();
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
            textCodigo.Text = "";
            textNombre.Text = "";
            textDescripcion.Text = "";
            comboCategoria.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;

            textCodigo.Select();
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
                    textCodigo.Text = datagridviewData.Rows[indice].Cells["codigo"].Value.ToString();
                    textNombre.Text = datagridviewData.Rows[indice].Cells["nombre"].Value.ToString();
                    textDescripcion.Text = datagridviewData.Rows[indice].Cells["descripcion"].Value.ToString();


                    foreach (OpcionCombo oc in comboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.valor) == Convert.ToInt32(datagridviewData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indiceCombo = comboCategoria.Items.IndexOf(oc);
                            comboCategoria.SelectedIndex = indiceCombo;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto obj = new Producto()
                    {
                        idProducto = Convert.ToInt32(textId.Text)
                    };


                    bool respuesta = new CN_Producto().Eliminar(obj, out mensaje);

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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (datagridviewData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in datagridviewData.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow row in datagridviewData.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                                row.Cells[2].Value.ToString(),
                                row.Cells[3].Value.ToString(),
                                row.Cells[4].Value.ToString(),
                                row.Cells[6].Value.ToString(),
                                row.Cells[7].Value.ToString(),
                                row.Cells[8].Value.ToString(),
                                row.Cells[9].Value.ToString(),
                                row.Cells[11].Value.ToString(),
                        });
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar el reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

            }
        }
    }
}
