using CapaAdmin.Models.DBEntidades;

namespace CapaAdmin.Models.Services.SerCategoria
{
    public interface ICategoria
    {
        public Task<IEnumerable<DBEntidades.Categoria>> Listar();

        public int Registrar(DBEntidades.Categoria oCategoria, out string mensaje);

        public bool Editar(DBEntidades.Categoria oCategoria, out string mensaje);

        public bool Delete(int id, out string mensaje);
    }
}
