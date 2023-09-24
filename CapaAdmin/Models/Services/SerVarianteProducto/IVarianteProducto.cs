namespace CapaAdmin.Models.Services.SerVarianteProducto
{
    public interface IVarianteProducto
    {
        public Task<IEnumerable<DBEntidades.VarianteProducto>> Listar();

    }
}
