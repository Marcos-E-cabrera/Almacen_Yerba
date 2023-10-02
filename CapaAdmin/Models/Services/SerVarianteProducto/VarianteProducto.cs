using CapaAdmin.Models.DBEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaAdmin.Models.Services.SerVarianteProducto
{
    public class VarianteProducto : IVarianteProducto
    {
        private readonly DbcarritoContext _context;

        public VarianteProducto(DbcarritoContext context)
        {
            _context = context;
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

    }
}
