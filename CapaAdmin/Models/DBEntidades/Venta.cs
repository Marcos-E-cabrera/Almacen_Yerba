using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int? IdCliente { get; set; }

    public int? TotalProducto { get; set; }

    public decimal? MontoTotal { get; set; }

    public string? Contacto { get; set; }

    public string? Telofono { get; set; }

    public string? Direccion { get; set; }

    public string? IdTransaccion { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
