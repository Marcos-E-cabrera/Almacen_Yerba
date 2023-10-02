using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapaAdmin.Models.DBEntidades;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? IdMarca { get; set; }

    public int? IdCategoria { get; set; }

    public string? RutaImage { get; set; }

    public string? NombreImagen { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<VarianteProducto> VarianteProductos { get; set; } = new List<VarianteProducto>();
}
