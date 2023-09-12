using CapaAdmin.Models.DBEntidades;

namespace CapaAdmin.Models.Services
{
    public interface IUsuarios
    {
        public Task<IEnumerable<Usuario>> Listar();
    }
}
