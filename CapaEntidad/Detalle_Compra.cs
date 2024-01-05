using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Detalle_Compra
    {
        public int idDetalleCompra {  get; set; }
        public Producto oProducto {  get; set; }
        public decimal precioCompra {  get; set; }
        public decimal precioVenta {  get; set; }
        public int cantidad {  get; set; }
        public decimal montoTotal {  get; set; }
        public string fechaRegistro { get; set; }
    }
}
