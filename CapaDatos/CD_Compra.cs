using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Compra
    {
        public int ObtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from COMPRA");
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

        public bool Registrar(Compra obj,DataTable DetalleCompra,out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", oConexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.idProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.tipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.numeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.montoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }


        public Compra ObtenerCompra(string numero)
        {
            Compra obj = new Compra();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompra,");
                    query.AppendLine("u.NombreCompleto,");
                    query.AppendLine("pr.Documento,pr.RazonSocial,");
                    query.AppendLine("c.TipoDocumento,c.NumeroDocumento,c.MontoTotal,convert(char(10), c.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("from COMPRA c");
                    query.AppendLine("inner join USUARIO u on u.IdUsuario = c.IdUsuario");
                    query.AppendLine("inner join PROVEEDOR pr on pr.IdProveedor = c.IdProveedor");
                    query.AppendLine("where c.NumeroDocumento = @numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@numero",numero);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            obj = new Compra()
                            {
                                idCompra = Convert.ToInt32(dr["IdCompra"]),
                                oUsuario = new Usuario() { nombreCompleto = dr["NombreCompleto"].ToString() },
                                oProveedor = new Proveedor() { documento = dr["Documento"].ToString(), razonSocial = dr["RazonSocial"].ToString() },
                                tipoDocumento = dr["TipoDocumento"].ToString(),
                                montoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                fechaRegistro = dr["FechaRegistro"].ToString(),
                                numeroDocumento = dr["NumeroDocumento"].ToString(),
                            };
                        }

                    }
                }
                catch (Exception)
                {
                    obj = new Compra();
                }
            }
            return obj;
        }


        public List<Detalle_Compra> ObtenerDetalleCompra(int idCompra)
        {
            List<Detalle_Compra> oLista = new List<Detalle_Compra>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cadena))
                {
                    conexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.Nombre,dc.PrecioCompra,dc.Cantidad,dc.MontoTotal");
                    query.AppendLine("from DETALLE_COMPRA dc");
                    query.AppendLine("inner join PRODUCTO p on p.IdProducto = dc.IdProducto");
                    query.AppendLine("where dc.IdCompra = @idCompra");

                    SqlCommand cmd = new SqlCommand(query.ToString(), conexion);
                    cmd.Parameters.AddWithValue("@idCompra", idCompra);
                    cmd.CommandType = System.Data.CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            oLista.Add(new Detalle_Compra()
                            {
                                oProducto = new Producto() { nombre = dr["Nombre"].ToString() },
                                precioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                montoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<Detalle_Compra>();
            }
            return oLista;
        }

    }
}
