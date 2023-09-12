using System;
using System.Collections.Generic;

namespace CapaAdmin.Models.DBEntidades;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? Reestablecer { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
