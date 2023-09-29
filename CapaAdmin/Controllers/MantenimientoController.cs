﻿using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services;
using CapaAdmin.Models.Services.SerCategoria;
using CapaAdmin.Models.Services.SerMarca;
using CapaAdmin.Models.Services.SerProductos;
using CapaAdmin.Models.Services.SerUsuarios;
using CapaAdmin.Models.Services.SerVarianteProducto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging.Signing;
using NuGet.Protocol;

namespace CapaAdmin.Controllers
{
    public class MantenimientoController : Controller
    {


        #region SERVICES 
        private readonly IConfiguration _configuration;
        private readonly ICategoria _contextCategoria;
        private readonly IMarcas _contextMarcas;
        private readonly IProductos _contextProducto;
        private readonly IVarianteProducto _contextVarianteProducto;
        private readonly IWebHostEnvironment _env;

        public MantenimientoController(ICategoria contextCategoria, IMarcas contextMarcas, IProductos contextProducto, IVarianteProducto contextVarianteProducto, IConfiguration configuration, IWebHostEnvironment env)
        {
            _contextCategoria = contextCategoria;
            _contextMarcas = contextMarcas;
            _contextProducto = contextProducto;
            _contextVarianteProducto = contextVarianteProducto;
            _configuration = configuration;
            _env = env;
        }
        #endregion

        // ---------------------------- CATEGORIA ----------------------------

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

        // ---------------------------- MARCAS ----------------------------

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

        // ---------------------------- PRODUCTOS ----------------------------

        public IActionResult Productos()
        {
            return View();
        }

        #region METODOS PRODUCTO

        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            var oProducto = await _contextProducto.Listar();


            var productoData = from p in oProducto
                               select new
                               {
                                   IdProducto = p.IdProducto,
                                   RutaImage = p.RutaImage,
                                   NombreImagen = p.NombreImagen,
                                   Nombre = p.Nombre,
                                   Descripcion = p.Descripcion,
                                   IdMarcaNavigation = p.IdMarcaNavigation != null ? p.IdMarcaNavigation.Descripcion : null,
                                   IdCategoriaNavigation = p.IdCategoriaNavigation != null ? p.IdCategoriaNavigation.Descripcion : null,
                                   IdMarca = p.IdMarca,
                                   IdCategoria = p.IdCategoria,
                                   Activo = p.Activo
                               };


            var jsonResult = JsonConvert.SerializeObject(new { data = productoData }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpPost]
        public async Task<IActionResult> GuardarProducto(Producto oProducto, IFormFile formFile)
        {
            string mensaje = string.Empty;
            bool operacionOk = true;
            bool guardarImagenOk = true;

            if (oProducto.IdProducto == 0)
            {
                var idProductoGenerado = _contextProducto.Registrar(oProducto, out mensaje);
                if (idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacionOk = false;
                }
            }
            else
            {
                operacionOk = _contextProducto.Editar(oProducto, out mensaje);
            }

            if (operacionOk)
            {
                if (formFile != null)
                {
                    string path = "C:\\AlmacenImagenes";
                    string fileName = "";

                    try
                    {
                        var filePath = Path.Combine(_env.WebRootPath, path, formFile.FileName);
                        using var stream = new FileStream(filePath, FileMode.Create);

                        await formFile.CopyToAsync(stream);
                        fileName = formFile.FileName;

                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarImagenOk = false;
                    }

                    if (guardarImagenOk)
                    {
                        oProducto.RutaImage = path;
                        oProducto.NombreImagen = fileName;
                        bool respuesta = _contextProducto.GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto, pero hubo un error con la imagen";
                    }
                }
            }

            var jsonResult = JsonConvert.SerializeObject(new { operacionOk = operacionOk, idGenerado = oProducto.IdProducto, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }


        [HttpPost]
        public async Task<IActionResult> ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto =  await _contextProducto.ListarById(id);
            string textoBase64 = Recursos.ConvertirBase64(Path.Combine(Path.Combine("C:\\AlmacenImagenes ", oProducto.RutaImage), oProducto.NombreImagen), out conversion);

            var jsonResult = JsonConvert
                .SerializeObject(new { 
                    conversion = conversion,
                    textoBase64 = textoBase64,
                    extension = Path.GetExtension(oProducto.NombreImagen)
                }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        #endregion

        // ---------------------------- VARIANTE DE PRODUCTO ----------------------------

        public IActionResult Variantes()
        {
            return View();
        }

        #region METODOS VARIANTE

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
        #endregion
    }
}
