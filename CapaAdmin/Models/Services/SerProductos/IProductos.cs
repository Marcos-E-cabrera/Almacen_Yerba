using CapaAdmin.Models.DBEntidades;

namespace CapaAdmin.Models.Services.SerProductos
{
    public interface IProductos
    {
        public Task<IEnumerable<Producto>> Listar();

    }
}
