using CapaAdmin.Models.DBEntidades;

namespace CapaAdmin.Models.Services.SerVarianteProducto
{
    public interface IVarianteProducto
    {
        public Task<IEnumerable<DBEntidades.VarianteProducto>> Listar();

        public Task<IEnumerable<DBEntidades.VarianteProducto>> ListarById(int id);

        public int Registrar(DBEntidades.VarianteProducto oVariante, out string mensaje);

        public bool Editar(DBEntidades.VarianteProducto oVariante, out string mensaje);

        public bool Delete(int id, out string mensaje);
    }
}
