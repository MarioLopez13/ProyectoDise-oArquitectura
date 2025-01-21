using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Auditoria
{
    public decimal IdAditoria { get; set; }

    public string DetalleAuditoria { get; set; } = null!;

    public DateTime FechaAuditoria { get; set; }

    public string Ubicacion { get; set; } = null!;

    public decimal IdTransaccion { get; set; }

    public virtual Transaccion IdTransaccionNavigation { get; set; } = null!;
}
