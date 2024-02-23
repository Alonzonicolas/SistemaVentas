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
    public partial class frmDetalleCompra : Form
    {
        public frmDetalleCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(textBusqueda.Text);

            if(oCompra.idCompra != 0)
            {
                textNumeroDocumento.Text = oCompra.numeroDocumento;

                textFecha.Text = oCompra.fechaRegistro;
                textTipoDoc.Text = oCompra.tipoDocumento;
                textUsuario.Text = oCompra.oUsuario.nombreCompleto;
                textDocProveedor.Text = oCompra.oProveedor.documento;
                textNombreProveedor.Text = oCompra.oProveedor.razonSocial;

                dataGridView1.Rows.Clear();

                foreach(Detalle_Compra dc in oCompra.oDetalleCompra)
                {
                    dataGridView1.Rows.Add(new object[] { dc.oProducto.nombre, dc.precioCompra, dc.cantidad, dc.montoTotal });
                }

                textMontoTotal.Text = oCompra.montoTotal.ToString("0.00");
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            textFecha.Text = "";
            textTipoDoc.Text = "";
            textUsuario.Text = "";
            textDocProveedor.Text = "";
            textNombreProveedor.Text = "";
            dataGridView1.Rows.Clear();
            textMontoTotal.Text = "0.00";
        }
    }
}
