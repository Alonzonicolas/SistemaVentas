using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class frmVentas : Form
    {
        private Usuario _Usuario;

        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            comboTipoDoc.Items.Add(new OpcionCombo() { valor = "Boleta", texto = "Boleta" });
            comboTipoDoc.Items.Add(new OpcionCombo() { valor = "Factura", texto = "Factura" });
            comboTipoDoc.DisplayMember = "texto";
            comboTipoDoc.ValueMember = "valor";
            comboTipoDoc.SelectedIndex = 0;

            textFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textIdProducto.Text = "0";
            textMontoPago.Text = "";
            textCambio.Text = "";
            textTotalPagar.Text = "0";
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    textDocCliente.Text = modal._Cliente.documento;
                    textNombreCliente.Text = modal._Cliente.nombreCompleto;
                    textCodProducto.Select();
                }
                else
                {
                    textDocCliente.Select();
                }
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    textIdProducto.Text = modal._Producto.idProducto.ToString();
                    textCodProducto.Text = modal._Producto.codigo;
                    textProducto.Text = modal._Producto.nombre;
                    textPrecioProd.Text = modal._Producto.precioVenta.ToString("0.00");
                    textStock.Text = modal._Producto.stock.ToString();
                    textCantidad.Select();
                }
                else
                {
                    textCodProducto.Select();
                }
            }
        }

        private void textCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().Listar().Where(p => p.codigo == textCodProducto.Text && p.estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    textCodProducto.BackColor = Color.Honeydew;
                    textIdProducto.Text = oProducto.idProducto.ToString();
                    textProducto.Text = oProducto.nombre;
                    textPrecioProd.Text = oProducto.precioVenta.ToString("0.00");
                    textStock.Text = oProducto.stock.ToString();
                    textCantidad.Select();
                }
                else
                {
                    textCodProducto.BackColor = Color.MistyRose;
                    textIdProducto.Text = "0";
                    textProducto.Text = "";
                    textPrecioProd.Text = "";
                    textStock.Text = "";
                    textCantidad.Value = 1;
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(textIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(textPrecioProd.Text, out precio))
            {
                MessageBox.Show("Precio - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textPrecioProd.Select();
                return;
            }

            if (Convert.ToInt32(textStock.Text) < Convert.ToInt32(textCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == textIdProducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {
                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(textIdProducto.Text),
                    Convert.ToInt32(textCantidad.Value.ToString())
                    );

                if(respuesta)
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                        textIdProducto.Text,
                        textProducto.Text,
                        precio.ToString("0.00"),
                        textCantidad.Value.ToString(),
                        (textCantidad.Value * precio).ToString("0.00")
                    });

                    calcularTotal();
                    limpiarProducto();
                    textCodProducto.Select();
                }
            }
        }


        private void calcularTotal()
        {
            decimal total = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            textTotalPagar.Text = total.ToString("0.00");
        }

        private void limpiarProducto()
        {
            textIdProducto.Text = "0";
            textCodProducto.Text = "";
            textCodProducto.BackColor = Color.White;
            textProducto.Text = "";
            textPrecioProd.Text = "";
            textStock.Text = "";
            textCantidad.Value = 1;
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var celda = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                var ancho = celda.Size.Width - 7;
                var alto = celda.Size.Height - 7;
                var x = e.CellBounds.Left + (e.CellBounds.Width - ancho) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - alto) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete25, new Rectangle(x, y, ancho, alto));
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dataGridView1.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dataGridView1.Rows[indice].Cells["Cantidad"].Value.ToString())
                    );

                    if(respuesta)
                    {
                        dataGridView1.Rows.RemoveAt(indice);
                        calcularTotal();
                    }
                }
            }
        }

        private void textPrecioProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (textPrecioProd.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void textMetodoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (textMontoPago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void calcularCambio()
        {
            if(textTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal total = Convert.ToDecimal(textTotalPagar.Text);

            if(textMontoPago.Text.Trim() == "")
            {
                textMontoPago.Text = "0";
            }

            if(decimal.TryParse(textMontoPago.Text.Trim(), out pagaCon))
            {
                if(pagaCon < total)
                {
                    textCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagaCon - total;
                    textCambio.Text = cambio.ToString("0.00");
                }
            }
        }

        private void textMontoPago_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(textDocCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(textNombreCliente.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos a la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IdProducto", typeof(int));
            detalle_venta.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                detalle_venta.Rows.Add(
                    new object[]
                    {
                        row.Cells["IdProducto"].Value.ToString(),
                        row.Cells["Precio"].Value.ToString(),
                        row.Cells["Cantidad"].Value.ToString(),
                        row.Cells["SubTotal"].Value.ToString()
                    });
            }

            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);
            calcularCambio();

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { idUsuario = _Usuario.idUsuario },
                tipoDocumento = ((OpcionCombo)comboTipoDoc.SelectedItem).texto,
                numeroDocumento = numeroDocumento,
                documentoCliente = textDocCliente.Text,
                nombreCliente = textNombreCliente.Text,
                montoPago = Convert.ToDecimal(textMontoPago.Text),
                montoCambio = Convert.ToDecimal(textCambio.Text),
                montoTotal = Convert.ToDecimal(textTotalPagar.Text),
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

            if(respuesta)
            {
                var result = MessageBox.Show("Número de venta generada:\n" + numeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(numeroDocumento);

                textDocCliente.Text = "";
                textNombreCliente.Text = "";
                dataGridView1.Rows.Clear();
                calcularTotal();
                textMontoPago.Text = "";
                textCambio.Text = "";
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
