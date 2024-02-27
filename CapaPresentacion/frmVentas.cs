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
        public frmVentas()
        {
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
            textMetodoPago.Text = "";
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
    }
}
