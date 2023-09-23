using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class VarianteProducto
{
    public int IdVariante { get; set; }

    public int? IdProducto { get; set; }

    public int? Gramos { get; set; }

    public decimal? Precio { get; set; }

    public int? Stock { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Producto? IdProductoNavigation { get; set; }
}
