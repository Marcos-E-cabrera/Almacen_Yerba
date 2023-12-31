﻿using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services.SerUsuarios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CapaAdmin.Models.Services.SerCategoria
{
    public class Categoria : ICategoria
    {
        private readonly DbcarritoContext _context;

        public Categoria(DbcarritoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DBEntidades.Categoria>> Listar()
        {
            try
            {
                var data = await _context.Categoria.ToListAsync();
                if (data == null)
                {
                    return new List<DBEntidades.Categoria>();
                }
                return data;
            }
            catch
            {
                return new List<Models.DBEntidades.Categoria>();
            }
        }
        
        public int Registrar(Models.DBEntidades.Categoria oCategoria, out string? mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            if (oCategoria == null)
            {
                mensaje = "El objeto Categoria es nulo.";
                return idGenerado;
            }

            if (string.IsNullOrEmpty(oCategoria.Descripcion))
            {
                mensaje = "El campo Descripcion es obligatorio.";
                return idGenerado;
            }

            try
            {            
                var paramDescripcion = new SqlParameter("@Descripcion", oCategoria.Descripcion);
                var paramActivo = new SqlParameter("@Activo", oCategoria.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_RegistrarCategoria @Descripcion, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramDescripcion, paramActivo, paramMensaje, paramResultado);

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
        
        public bool Editar(DBEntidades.Categoria oCategoria, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            if (oCategoria == null)
            {
                mensaje = "El objeto Categoria es nulo.";
                return resultado;
            }

            if (string.IsNullOrEmpty(oCategoria.Descripcion))
            {
                mensaje = "El campo Descripcion es obligatorio.";
                return resultado;
            }

            try
            {
                var paramId = new SqlParameter("@IdCategoria", oCategoria.IdCategoria);
                var paramDescripcion = new SqlParameter("@Descripcion", oCategoria.Descripcion);
                var paramActivo = new SqlParameter("@Activo", oCategoria.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_EditarCategoria @IdCategoria, @Descripcion, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramDescripcion, paramActivo, paramMensaje, paramResultado);

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

        public bool Delete(int id, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                var paramId = new SqlParameter("@IdCategoria", id);
              
                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_EliminarCategoria @IdCategoria, @Mensaje OUTPUT, @Resultado OUTPUT";
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



    }
}
