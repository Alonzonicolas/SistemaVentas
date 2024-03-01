using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Venta
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VENTA");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception)
                {
                    idCorrelativo = 0;
                }
            }
            return idCorrelativo;
        }

        public bool RestarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock - @cantidad where IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool SumarStock(int idproducto, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRODUCTO set Stock = Stock + @cantidad where IdProducto = @idproducto");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@idproducto", idproducto);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;

                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarVenta", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.tipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.numeroDocumento);
                    cmd.Parameters.AddWithValue("DocumentoCliente", obj.documentoCliente);
                    cmd.Parameters.AddWithValue("NombreCliente", obj.nombreCliente);
                    cmd.Parameters.AddWithValue("MontoPago", obj.montoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.montoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.montoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }
            
            return Respuesta;
        }

    }
}
