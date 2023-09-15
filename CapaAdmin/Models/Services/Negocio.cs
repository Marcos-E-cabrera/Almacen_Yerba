using CapaAdmin.Models.DBEntidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Data;

namespace CapaAdmin.Models.Services
{
    public class Negocio : IUsuarios
    {
        private readonly DbcarritoContext _context;
        public Negocio(DbcarritoContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Usuario>> Listar()
        {
            try
            {
                var data = await _context.Usuarios.ToListAsync();
                if ( data == null)
                {
                    return new List<Usuario>();
                }
                return data;
            }
            catch
            {
                return new List<Usuario>();
            }
        }

        public int Registrar(Usuario oUsuario, out string mensaje)
        {
            int idGenerado = 0;
            mensaje = string.Empty;

            if (oUsuario == null)
            {
                mensaje = "El objeto Usuario es nulo.";
                return idGenerado;
            }

            if (string.IsNullOrEmpty(oUsuario.Nombre))
            {
                mensaje = "El campo Nombre es obligatorio.";
                return idGenerado;
            }
            else if (string.IsNullOrEmpty(oUsuario.Apellido))
            {
                mensaje = "El campo Apellido es obligatorio.";
                return idGenerado;
            }
            else if (string.IsNullOrEmpty(oUsuario.Email))
            {
                mensaje = "El campo Email es obligatorio.";
                return idGenerado;
            }


            try
            {
                var password = "test123";
                oUsuario.Password = Validacion.GetSHA2S6(password);

                var paramNombre = new SqlParameter("@Nombre", oUsuario.Nombre);
                var paramApellido = new SqlParameter("@Apellido", oUsuario.Apellido);
                var paramEmail = new SqlParameter("@Email", oUsuario.Email);
                var paramPassword = new SqlParameter("@Password", oUsuario.Password);
                var paramActivo = new SqlParameter("@Activo", oUsuario.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_RegistrarUsuario @Nombre, @Apellido, @Email, @Password, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramNombre, paramApellido, paramEmail, paramPassword, paramActivo, paramMensaje, paramResultado);

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

        public bool Editar(Usuario oUsuario, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            if (oUsuario == null)
            {
                mensaje = "El objeto Usuario es nulo.";
                return resultado;
            }

            if (string.IsNullOrEmpty(oUsuario.Nombre))
            {
                mensaje = "El campo Nombre es obligatorio.";
                return resultado;
            }
            else if (string.IsNullOrEmpty(oUsuario.Apellido))
            {
                mensaje = "El campo Apellido es obligatorio.";
                return resultado;
            }
            else if (string.IsNullOrEmpty(oUsuario.Email))
            {
                mensaje = "El campo Email es obligatorio.";
                return resultado;
            }


            try
            {
                var paramId = new SqlParameter("@IdUsuario", oUsuario.IdUsuario);
                var paramNombre = new SqlParameter("@Nombre", oUsuario.Nombre);
                var paramApellido = new SqlParameter("@Apellido", oUsuario.Apellido);
                var paramEmail = new SqlParameter("@Email", oUsuario.Email);
                var paramActivo = new SqlParameter("@Activo", oUsuario.Activo);

                var paramMensaje = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                var paramResultado = new SqlParameter("@Resultado", SqlDbType.Bit);
                paramResultado.Direction = ParameterDirection.Output;

                var query = "EXEC sp_EditarUsuario @IdUsuario, @Nombre, @Apellido, @Email, @Activo, @Mensaje OUTPUT, @Resultado OUTPUT";
                _context.Database.ExecuteSqlRaw(query, paramId, paramNombre, paramApellido, paramEmail, paramActivo, paramMensaje, paramResultado);

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
                var oUsuario =  _context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
                if (oUsuario == null)
                {
                    mensaje = "El objeto Usuario es nulo.";
                    return resultado;
                }
            
                _context.Remove(oUsuario);
                _context.SaveChanges();

                resultado = true;
            }
            catch (Exception ex) 
            {
                mensaje = ex.Message;
                resultado = false;
            }

            return resultado;
        }
    }
}
