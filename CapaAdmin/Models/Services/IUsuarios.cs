using CapaAdmin.Models.DBEntidades;

namespace CapaAdmin.Models.Services
{
    public interface IUsuarios
    {
        public Task<IEnumerable<Usuario>> Listar();

        public int Registrar(Usuario oUsuario, out string mensaje);

        public bool Editar(Usuario oUsuario, out string mensaje);

        public bool Delete(int id, out string mensaje);

    }
}
