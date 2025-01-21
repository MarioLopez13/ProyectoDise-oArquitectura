using System;
using System.Collections.Generic;

namespace DiseñoArquitecturaProyecto.Models.DB;

public partial class Transaccion
{
    public decimal IdTransaccion { get; set; }

    public DateTime FechaTransaccion { get; set; }

    public decimal Monto { get; set; }

    public string Estado { get; set; } = null!;

    public decimal IdOrigenCli { get; set; }

    public decimal IdCliente { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; set; } = new List<Auditoria>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
