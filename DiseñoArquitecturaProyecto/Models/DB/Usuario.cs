using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Usuario
{
    public decimal IdUsuario { get; set; }

    public decimal Cedula { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string Estado { get; set; } = null!;
}
