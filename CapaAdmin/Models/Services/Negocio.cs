using CapaAdmin.Models.DBEntidades;
using Microsoft.EntityFrameworkCore;

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

    }
}
