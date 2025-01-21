using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Reporte
{
    public decimal IdReporte { get; set; }

    public DateTime FechaReporte { get; set; }

    public string Contenido { get; set; } = null!;

    public decimal IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
