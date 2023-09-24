using CapaAdmin.Models.DBEntidades;
using Microsoft.EntityFrameworkCore;
using System;
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
   
    }
}
