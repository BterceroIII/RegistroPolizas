using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class Poliza
{
    public int IdPoliza { get; set; }

    public string? Codigo { get; set; }

    public string? EmpresaProveedora { get; set; }

    public string? Tipo { get; set; }

    public int? Eliminado { get; set; }

    public virtual ICollection<ContratoPoliza> ContratoPolizas { get; } = new List<ContratoPoliza>();
}
