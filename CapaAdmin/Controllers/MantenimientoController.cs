﻿using Microsoft.AspNetCore.Mvc;

namespace CapaAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        public IActionResult Categorias()
        {
            return View();
        }

        public IActionResult Marcas()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }
    }
}
