using CapaEntidad;
using CapaNegocio;
using DocumentFormat.OpenXml.Spreadsheet;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

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

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if(textTipoDoc.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            Negocio oDatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", textTipoDoc.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", textNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@docproveedor", textDocProveedor.Text);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", textNombreProveedor.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", textFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", textUsuario.Text);

            string filas = string.Empty;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", textMontoTotal.Text);


            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Compra_{0}.pdf", textNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                using(FileStream stream = new FileStream(saveFile.FileName, FileMode.Create)) {
                    Document pdfDoc = new Document(PageSize.A4,25,25,25,25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if(obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left,pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer,pdfDoc,sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
