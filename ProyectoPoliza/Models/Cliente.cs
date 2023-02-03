using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string TipoDocIdentidad { get; set; } = null!;

    public string NoDocIdentidad { get; set; } = null!;

    public string? Nacionalidad { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public int? Eliminado { get; set; }

    public virtual ICollection<ContratoPoliza> ContratoPolizas { get; } = new List<ContratoPoliza>();

    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
