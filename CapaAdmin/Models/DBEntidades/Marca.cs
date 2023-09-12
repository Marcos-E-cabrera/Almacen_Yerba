using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
