namespace CapaAdmin.Models.Services.SerVarianteProducto
{
    public interface IVarianteProducto
    {
        public Task<IEnumerable<DBEntidades.VarianteProducto>> Listar();

        public Task<IEnumerable<DBEntidades.VarianteProducto>> ListarById(int id);

    }
}
