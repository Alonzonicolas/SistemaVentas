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
    public partial class frmCompras : Form
    {
        private Usuario _Usuario;

        public frmCompras(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            comboTipoDoc.Items.Add(new OpcionCombo() { valor = "Boleta", texto = "Boleta" });
            comboTipoDoc.Items.Add(new OpcionCombo() { valor = "Factura", texto = "Factura" });
            comboTipoDoc.DisplayMember = "texto";
            comboTipoDoc.ValueMember = "valor";
            comboTipoDoc.SelectedIndex = 0;

            textFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            textIdProveedor.Text = "0";
            textIdProducto.Text = "0";
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    textIdProveedor.Text = modal._Proveedor.idProveedor.ToString();
                    textDocProveedor.Text = modal._Proveedor.documento;
                    textNombreProveedor.Text = modal._Proveedor.razonSocial;
                }
                else
                {
                    textDocProveedor.Select();
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
                    textPrecioCompra.Select();
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
                    textPrecioCompra.Select();
                }
                else
                {
                    textCodProducto.BackColor = Color.MistyRose;
                    textIdProducto.Text = "0";
                    textProducto.Text = "";
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool productoExiste = false;

            if(int.Parse(textIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(textPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("Precio Compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textPrecioCompra.Select();
                return;
            }

            if (!decimal.TryParse(textPrecioVenta.Text, out precioVenta))
            {
                MessageBox.Show("Precio Venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textPrecioVenta.Select();
                return;
            }

            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == textIdProducto.Text)
                {
                    productoExiste = true;
                    break;
                }
            }

            if (!productoExiste)
            {
                dataGridView1.Rows.Add(new object[]
                {
                    textIdProducto.Text,
                    textProducto.Text,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    textCantidad.Value.ToString(),
                    (textCantidad.Value * precioCompra).ToString("0.00")
                });

                calcularTotal();
                limpiarProducto();
                textCodProducto.Select();
            }
        }

        private void limpiarProducto()
        {
            textIdProducto.Text = "0";
            textCodProducto.Text = "";
            textCodProducto.BackColor = Color.White;
            textProducto.Text = "";
            textPrecioCompra.Text = "";
            textPrecioVenta.Text = "";
            textCantidad.Value = 1;
        }


        private void calcularTotal()
        {
            decimal total = 0;
            if(dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            textTotalPagar.Text = total.ToString("0.00");
        }

    }
}
