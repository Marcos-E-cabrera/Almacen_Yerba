using CapaAdmin.Models.DBEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapaAdmin.Models.Services.SerProductos
{
    public class Productos : IProductos
    {
        private readonly DbcarritoContext _context;

        public Productos(DbcarritoContext context)
        {
            _context = context;
        }

        public int Registrar(Producto oProducto, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            if (oProducto == null)
            {
                mensaje = "El objeto Producto es nulo.";
                return idGenerado;
            }

            if (string.IsNullOrEmpty(oProducto.Nombre))
            {
                mensaje = "El Nombre del Producto es obligatiorio.";
                return idGenerado;
            }
            else if (string.IsNullOrEmpty(oProducto.Descripcion))
            {
                mensaje = "La Descripcion del producto es obligatorio.";
                return idGenerado;
            }
            else if (oProducto.IdMarca == 0)
            {
                mensaje = "La Marca del producto es obligatorio.";
                return idGenerado;
            }
            else if (oProducto.IdCategoria == 0)
            {
                mensaje = "La Categoria del producto es obligatorio.";
                return idGenerado;
            }

            try
            {
                var paramNombre = new SqlParameter("@Nombre", oProducto.Nombre);
                var paramDescripcion = new SqlParameter("@Descripcion", oProducto.Descripcion);
                var paramIdMarca = new SqlParameter("@IdMarca", oProducto.IdMarca);
                var paramIdCategoria = new SqlParameter("@IdCategoria", oProducto.IdCategoria);
                var paramActivo = new SqlParameter("@Activo", oProducto.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_RegistrarProducto @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramNombre, paramDescripcion, paramIdMarca, paramIdCategoria, paramActivo, paramMensaje, paramResultado);

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

        public bool GuardarDatosImagen( Producto oProducto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
           
            if (oProducto == null)
            {
                mensaje = "El objeto Producto es nulo.";
                return resultado;
            }

            try
            {
                var dataProducto =  _context.Productos.FirstOrDefault(x => x.IdProducto == oProducto.IdProducto);

                if (dataProducto != null)
                {
                    resultado = true;

                    dataProducto.RutaImage = oProducto.RutaImage;
                    dataProducto.NombreImagen = oProducto.NombreImagen;

                    _context.SaveChanges();
                    
                }

            }
            catch (Exception ex)
            {
                mensaje = "Error desconocido: " + ex.Message;
                resultado = false;
            }

            return resultado;
        }

        public bool Editar(Producto oProducto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            if (oProducto == null)
            {
                mensaje = "El objeto Producto es nulo.";
                return resultado;
            }

            if (string.IsNullOrEmpty(oProducto.Nombre))
            {
                mensaje = "El Nombre del Producto es obligatiorio.";
                return resultado;
            }
            else if (string.IsNullOrEmpty(oProducto.Descripcion))
            {
                mensaje = "La Descripcion del producto es obligatorio.";
                return resultado;
            }
            else if (oProducto.IdMarca == 0)
            {
                mensaje = "La Marca del producto es obligatorio.";
                return resultado;
            }
            else if (oProducto.IdCategoria == 0)
            {
                mensaje = "La Categoria del producto es obligatorio.";
                return resultado;
            }


            try
            {
                var paramId = new SqlParameter("@IdProducto", oProducto.IdProducto);
                var paramNombre = new SqlParameter("@Nombre", oProducto.Nombre);
                var paramDescripcion = new SqlParameter("@Descripcion", oProducto.Descripcion);
                var paramIdMarca = new SqlParameter("@IdMarca", oProducto.IdMarca);
                var paramIdCategoria = new SqlParameter("@IdCategoria", oProducto.IdCategoria);
                var paramActivo = new SqlParameter("@Activo", oProducto.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_ModificarProducto @IdProducto, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramNombre, paramDescripcion, paramIdMarca, paramIdCategoria, paramActivo, paramMensaje, paramResultado);

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

        public async Task<IEnumerable<Producto>> Listar()
        {
            try
            {
                var data = await _context.Productos
                    .Include(x => x.IdMarcaNavigation)
                    .Include(x => x.IdCategoriaNavigation)
                    .ToListAsync();
                                       
                if (data == null)
                {
                    return new List<Producto>();
                }

                return data;
                  
            }
            catch
            {
                return new List<Producto>();
            }
        }

        public async Task<Producto> ListarById( int id)
        {
            var data = await _context.Productos
                .Include(x => x.IdMarcaNavigation)
                .Include(x => x.IdCategoriaNavigation)
                .Where(x => x.IdProducto == id)
                .FirstOrDefaultAsync();

            return data;
        }
    }
}
