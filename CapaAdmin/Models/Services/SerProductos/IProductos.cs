using CapaAdmin.Models.DBEntidades;
using Microsoft.EntityFrameworkCore;

namespace CapaAdmin.Models.Services.SerProductos
{
    public interface IProductos
    {
        public Task<IEnumerable<Producto>> Listar();

        public Task<Producto> ListarById(int id);

        public int Registrar(DBEntidades.Producto oProducto, out string mensaje);

        public bool Editar(DBEntidades.Producto oProducto, out string mensaje);

        public bool GuardarDatosImagen(Producto oProducto, out string mensaje);

        public bool Delete(int id, out string mensaje);

    }
}
