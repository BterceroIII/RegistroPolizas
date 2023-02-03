using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class ContratoPoliza
{
    public int IdContrato { get; set; }

    public int? IdEmpleadoGestor { get; set; }

    public int? IdEmpleadoTramitador { get; set; }

    public int? IdCliente { get; set; }

    public int? IdPoliza { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public double? CostoPoliza { get; set; }

    public string? OrigenFondoPago { get; set; }

    public double? CobroProveedor { get; set; }

    public double? ComisionEmpresa { get; set; }

    public double? ComisionGestor { get; set; }

    public byte[]? ImagenContrato { get; set; }

    public string? Comentario { get; set; }

    public string? Estado { get; set; }

    public int? Eliminado { get; set; }

    public virtual ICollection<DetalleContrato> DetalleContratos { get; } = new List<DetalleContrato>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Empleado? IdEmpleadoGestorNavigation { get; set; }

    public virtual Empleado? IdEmpleadoTramitadorNavigation { get; set; }

    public virtual Poliza? IdPolizaNavigation { get; set; }
}
