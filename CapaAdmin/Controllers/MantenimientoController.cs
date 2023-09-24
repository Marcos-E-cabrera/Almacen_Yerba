using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services.SerCategoria;
using CapaAdmin.Models.Services.SerMarca;
using CapaAdmin.Models.Services.SerProductos;
using CapaAdmin.Models.Services.SerUsuarios;
using CapaAdmin.Models.Services.SerVarianteProducto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CapaAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly ICategoria _contextCategoria;
        private readonly IMarcas _contextMarcas;
        private readonly IProductos _contextProducto;
        private readonly IVarianteProducto _contextVarianteProducto;

        public MantenimientoController(ICategoria contextCategoria, IMarcas contextMarcas, IProductos contextProducto, IVarianteProducto contextVarianteProducto = null)
        {
            _contextCategoria = contextCategoria;
            _contextMarcas = contextMarcas;
            _contextProducto = contextProducto;
            _contextVarianteProducto = contextVarianteProducto;
        }

        public IActionResult Categorias()
        {
            return View();
        }

        #region METODOS CATEGORIA
        [HttpGet]
        public async Task<IActionResult> ListarCategoria()
        {
            var oCategoria = await _contextCategoria.Listar();

            // Serializa los datos y aplica formato para hacerlos legibles
            var jsonResult = JsonConvert.SerializeObject(new { data = oCategoria }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpPost]
        public IActionResult GuardarCategoria(Models.DBEntidades.Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = _contextCategoria.Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = _contextCategoria.Editar(objeto, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpDelete]
        public IActionResult EliminarCategoria(Models.DBEntidades.Categoria objeto)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            if (objeto.IdCategoria != 0)
            {
                resultado = _contextCategoria.Delete(objeto.IdCategoria, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }
        #endregion

        public IActionResult Marcas()
        {
            return View();
        }

        #region METODOS MARCAS
        [HttpGet]
        public async Task<IActionResult> ListarMarcas()
        {
            var oMarca = await _contextMarcas.Listar();

            // Serializa los datos y aplica formato para hacerlos legibles
            var jsonResult = JsonConvert.SerializeObject(new { data = oMarca }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpPost]
        public IActionResult GuardarMarca(Models.DBEntidades.Marca objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdMarca == 0)
            {
                resultado = _contextMarcas.Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = _contextMarcas.Editar(objeto, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpDelete]
        public IActionResult EliminarMarca(Models.DBEntidades.Marca objeto)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            if (objeto.IdMarca != 0)
            {
                resultado = _contextMarcas.Delete(objeto.IdMarca, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }
        #endregion

        public IActionResult Productos()
        {
            return View();
        }

        
        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            var oProducto = await _contextProducto.Listar();


            var productoData = oProducto.Select(p => new
            {
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                MarcaDescripcion = p.IdMarcaNavigation != null // Verifica si IdMarcaNavigation no es nulo
                                 ? p.IdMarcaNavigation.Descripcion // Si no es nulo, asigna la Descripcion de IdMarcaNavigation a MarcaDescripcion
                                 : null, // Si es nulo, asigna null a MarcaDescripcion
                CategoriaDescripcion = p.IdCategoriaNavigation != null ? p.IdCategoriaNavigation.Descripcion : null,
                Activo = p.Activo
            });

            var jsonResult = JsonConvert.SerializeObject(new { data = productoData }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        public IActionResult Variantes()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarVarianteProducto()
        {
            var oVarianteProducto = await _contextVarianteProducto.Listar();
            var VarianteProductoData = oVarianteProducto.Select(p => new
            {
                Producto = p.IdProductoNavigation != null ? p.IdProductoNavigation.Nombre:null,
                Gramos = p.Gramos,
                Precio = p.Precio,
                Stock = p.Stock,
                Activo = p.Activo,
            });
            var jsonResult = JsonConvert.SerializeObject(new { data = VarianteProductoData }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

    }
}
