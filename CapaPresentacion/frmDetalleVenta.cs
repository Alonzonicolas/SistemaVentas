using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (textTipoDoc.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", textTipoDoc.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", textNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@doccliente", textDocCliente.Text);
            Texto_Html = Texto_Html.Replace("@nombrecliente", textNombreCliente.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", textFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", textUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", textMontoTotal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", textMontoPago.Text);
            Texto_Html = Texto_Html.Replace("@cambio", textMontoCambio.Text);


            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Venta_{0}.pdf", textNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
