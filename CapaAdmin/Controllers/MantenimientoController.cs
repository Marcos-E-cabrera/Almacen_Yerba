using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services.SerCategoria;
using CapaAdmin.Models.Services.SerMarca;
using CapaAdmin.Models.Services.SerUsuarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CapaAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly ICategoria _contextCategoria;
        private readonly IMarcas _contextMarcas;

        public MantenimientoController(ICategoria contextCategoria, IMarcas contextMarcas)
        {
            _contextCategoria = contextCategoria;
            _contextMarcas = contextMarcas;
        }

        public IActionResult Categorias()
        {
            return View();
        }

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

        public IActionResult Marcas()
        {
            return View();
        }

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

        public IActionResult Productos()
        {
            return View();
        }
    }
}
