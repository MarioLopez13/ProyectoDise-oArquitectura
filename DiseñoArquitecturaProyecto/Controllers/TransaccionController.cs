using DiseñoArquitecturaProyecto.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace DiseñoArquitecturaProyecto.Controllers
{
    public class TransaccionController : Controller
    {
        private static readonly List<Transaccion> transacciones = new()
    {
        new Transaccion { IdTransaccion = 1, Estado = "Aprobada", Monto = 100, FechaTransaccion = DateTime.Now, IdCliente = 1, IdOrigenCli = 1 },
        new Transaccion { IdTransaccion = 2, Estado = "Rechazada", Monto = 200, FechaTransaccion = DateTime.Now, IdCliente = 2, IdOrigenCli = 2 },
        new Transaccion { IdTransaccion = 3, Estado = "Pendiente", Monto = 300, FechaTransaccion = DateTime.Now, IdCliente = 3, IdOrigenCli = 3 }
    };

        private static readonly List<Log> logs = new();

        public IActionResult Validar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validar(decimal id)
        {
            var transaccion = transacciones.FirstOrDefault(t => t.IdTransaccion == id);

            if (transaccion != null)
            {
                logs.Add(new Log
                {
                    IdLog = logs.Count + 1,
                    Evento = "Validación exitosa",
                    Detalle = $"Transacción encontrada. Estado: {transaccion.Estado}",
                    IdUsuario = 1
                });

                ViewBag.Resultado = $"<div class='alert alert-success'>Transacción encontrada. Estado: {transaccion.Estado}</div>";
            }
            else
            {
                logs.Add(new Log
                {
                    IdLog = logs.Count + 1,
                    Evento = "Validación fallida",
                    Detalle = "Transacción inexistente",
                    IdUsuario = 1
                });

                ViewBag.Resultado = "<div class='alert alert-danger'>Transacción inexistente</div>";
            }

            return View();
        }
    }

}
