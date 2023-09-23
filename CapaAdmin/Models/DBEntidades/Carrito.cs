using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdCliente { get; set; }

    public int? IdVariante { get; set; }

    public int? Cantidad { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual VarianteProducto? IdVarianteNavigation { get; set; }
}
