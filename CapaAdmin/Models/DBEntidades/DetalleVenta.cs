using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdVariante { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public virtual VarianteProducto? IdVarianteNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
