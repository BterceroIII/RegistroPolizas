using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdEmpleado { get; set; }

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int? Eliminado { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
}
