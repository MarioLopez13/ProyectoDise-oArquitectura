using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Cliente
{
    public decimal IdCliente { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public decimal Cedula { get; set; }

    public string Contrasea { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public decimal TotalCuenta { get; set; }

    public string Direccion { get; set; } = null!;

    public decimal Cuenta { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
