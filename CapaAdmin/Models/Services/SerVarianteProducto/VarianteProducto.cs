using CapaAdmin.Models.DBEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CapaAdmin.Models.Services.SerVarianteProducto
{
    public class VarianteProducto : IVarianteProducto
    {
        private readonly DbcarritoContext _context;

        public VarianteProducto(DbcarritoContext context)
        {
            _context = context;
        }

        public bool Delete(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                var paramId = new SqlParameter("@IdVariante", id);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_EliminarVarianteProducto @IdVariante, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramMensaje, paramResultado);

                resultado = (bool)paramResultado.Value;
                mensaje = paramMensaje.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = "Error de base de datos: " + ex.Message;
                resultado = false;
            }
            catch (Exception ex)
            {
                mensaje = "Error desconocido: " + ex.Message;
                resultado = false;
            }

            return resultado;
        }

        public bool Editar(DBEntidades.VarianteProducto oVariante, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            if (oVariante == null)
            {
                mensaje = "El objeto Producto es nulo.";
                return resultado;
            }
            else if (oVariante.IdProducto == 0)
            {
                mensaje = "El producto es obligatorio.";
                return resultado;
            }
            else if (oVariante.Precio <= 0)
            {
                mensaje = "La precio del producto es obligatorio.";
                return resultado;
            }
            else if (oVariante.Stock <= 0)
            {
                mensaje = "El Stock es obligatorio.";
                return resultado;
            }
            else if (oVariante.Gramos <= 0)
            {
                mensaje = "Los gramos del producto es obligatorio.";
                return resultado;
            }


            try
            {
                var paramGramos = new SqlParameter("@Gramos", oVariante.Gramos);
                var paramPrecio = new SqlParameter("@Precio", oVariante.Precio);
                var paramStock = new SqlParameter("@Stock", oVariante.Stock);
                var paramActivo = new SqlParameter("@Activo", oVariante.Activo); // Pasar como parámetro normal, no como output
                var paramId = new SqlParameter("@IdVariante", oVariante.IdVariante);


                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_ModificarVarianteProducto @IdVariante, @Gramos, @Precio, @Stock, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramGramos, paramPrecio, paramStock, paramActivo, paramMensaje, paramResultado);

                resultado = (bool)paramResultado.Value;
                mensaje = paramMensaje.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = "Error de base de datos: " + ex.Message;
                resultado = false;
            }
            catch (Exception ex)
            {
                mensaje = "Error desconocido: " + ex.Message;
                resultado = false;
            }

            return resultado;
        }

        public async Task<IEnumerable<DBEntidades.VarianteProducto>> Listar()
        {
            try
            {
                var data = await _context.VarianteProductos.Include(x => x.IdProductoNavigation).ToListAsync();

                if (data == null)
                {
                    return new List<DBEntidades.VarianteProducto>();
                }

                return data;

            }
            catch
            {
                return new List<DBEntidades.VarianteProducto>();
            }
        }

        public async Task<IEnumerable<DBEntidades.VarianteProducto>> ListarById(int id)
        {
            var data = await _context.VarianteProductos
                .Where( x => x.IdProducto == id)
                .Include(x => x.IdProductoNavigation)
                .ToListAsync();

            return data;
        }

        public int Registrar(DBEntidades.VarianteProducto oVariante, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            if (oVariante == null)
            {
                mensaje = "El objeto Producto es nulo.";
                return idGenerado;
            }
            else if (oVariante.IdProducto == 0)
            {
                mensaje = "El producto es obligatorio.";
                return idGenerado;
            }
            else if (oVariante.Precio <= 0)
            {
                mensaje = "La precio del producto es obligatorio.";
                return idGenerado;
            }
            else if (oVariante.Stock <= 0)
            {
                mensaje = "El Stock es obligatorio.";
                return idGenerado;
            }
            else if (oVariante.Gramos <= 0)
            {
                mensaje = "Los gramos del producto es obligatorio.";
                return idGenerado;
            }

            try
            {
                var paramId = new SqlParameter("@IdProducto", oVariante.IdProducto);
                var paramGramos = new SqlParameter("@Gramos", oVariante.Gramos);
                var paramPrecio = new SqlParameter("@Precio", oVariante.Precio);
                var paramStock = new SqlParameter("@Stock", oVariante.Stock);
                var paramActivo = new SqlParameter("@Activo", oVariante.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_RegistrarVarianteProducto @IdProducto, @Gramos, @Precio, @Stock, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramGramos, paramPrecio, paramStock, paramActivo, paramMensaje, paramResultado);

                idGenerado = (int)paramResultado.Value;
                mensaje = paramMensaje.Value.ToString();
            }
            catch (SqlException ex)
            {
                mensaje = "Error de base de datos: " + ex.Message;
                idGenerado = 0;
            }
            catch (Exception ex)
            {
                mensaje = "Error desconocido: " + ex.Message;
                idGenerado = 0;
            }

            return idGenerado;
        }
    }
}
