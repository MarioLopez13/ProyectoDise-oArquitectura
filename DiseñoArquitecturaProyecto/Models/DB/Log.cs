using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Log
{
    public decimal IdLog { get; set; }

    public string Evento { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public decimal IdUsuario { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
