namespace CapaAdmin.Models.Services.SerMarca
{
    public interface IMarcas
    {
        public Task<IEnumerable<DBEntidades.Marca>> Listar();

        public int Registrar(DBEntidades.Marca oCategoria, out string mensaje);

        public bool Editar(DBEntidades.Marca oCategoria, out string mensaje);

        public bool Delete(int id, out string mensaje);
    }
}
