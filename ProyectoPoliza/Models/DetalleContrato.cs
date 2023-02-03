using System;
using System.Collections.Generic;

namespace ProyectoPoliza.Models;

public partial class DetalleContrato
{
    public int IdDetalle { get; set; }

    public int? IdContrato { get; set; }

    public int? IdVehiculo { get; set; }

    public int? Eliminado { get; set; }

    public virtual ContratoPoliza? IdContratoNavigation { get; set; }

    public virtual Vehiculo? IdVehiculoNavigation { get; set; }
}
