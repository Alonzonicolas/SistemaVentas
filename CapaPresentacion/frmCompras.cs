using CapaEntidad;
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
            using(var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if(result == DialogResult.OK)
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
    }
}
