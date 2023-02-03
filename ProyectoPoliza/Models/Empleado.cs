using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? Nombre { get; set; }

    public string? Cedula { get; set; }

    public string? Telefono { get; set; }

    public string? Cargo { get; set; }

    public int? Eliminado { get; set; }

    public virtual ICollection<ContratoPoliza> ContratoPolizaIdEmpleadoGestorNavigations { get; } = new List<ContratoPoliza>();

    public virtual ICollection<ContratoPoliza> ContratoPolizaIdEmpleadoTramitadorNavigations { get; } = new List<ContratoPoliza>();

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
