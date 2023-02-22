using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public int? IdCliente { get; set; }

    public string? Tipo { get; set; }

    public string? NoCirculacion { get; set; }

    public string? NoPlaca { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? NoMotor { get; set; }

    public string? NoChasis { get; set; }

    public string? Uso { get; set; }

    public string? Año { get; set; }

    public int? Eliminado { get; set; }

    public virtual ICollection<DetalleContrato> DetalleContratos { get; } = new List<DetalleContrato>();

    public  Cliente refCliente { get; set; }
}
