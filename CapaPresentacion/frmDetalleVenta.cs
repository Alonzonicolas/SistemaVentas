using CapaEntidad;
using CapaNegocio;
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
    public partial class frmDetalleVenta : Form
    {
        public frmDetalleVenta()
        {
            InitializeComponent();
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            textBusqueda.Select();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CN_Venta().ObtenerVenta(textBusqueda.Text);

            if(oVenta.idVenta != 0)
            {
                textNumeroDocumento.Text = oVenta.numeroDocumento;

                textFecha.Text = oVenta.fechaRegistro;
                textTipoDoc.Text = oVenta.tipoDocumento;
                textUsuario.Text = oVenta.oUsuario.nombreCompleto;

                textDocCliente.Text = oVenta.documentoCliente;
                textNombreCliente.Text = oVenta.nombreCliente;

                dataGridView1.Rows.Clear();
                foreach (Detalle_Venta dv in oVenta.oDetalleVenta)
                {
                    dataGridView1.Rows.Add(new object[] { dv.oProducto.nombre, dv.precioVenta, dv.cantidad, dv.subTotal });
                }

                textMontoTotal.Text = oVenta.montoTotal.ToString("0.00");
                textMontoPago.Text = oVenta.montoPago.ToString("0.00");
                textMontoCambio.Text = oVenta.montoCambio.ToString("0.00");
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            textFecha.Text = "";
            textTipoDoc.Text = "";
            textUsuario.Text = "";
            textDocCliente.Text = "";
            textNombreCliente.Text = "";
            dataGridView1.Rows.Clear();
            textMontoTotal.Text = "0.00";
            textMontoPago.Text = "0.00";
            textMontoCambio.Text = "0.00";
        }

    }
}
