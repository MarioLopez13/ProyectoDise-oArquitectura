using DiseñoArquitecturaProyecto.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace DiseñoArquitecturaProyecto.Controllers
{
    public class AuditoriaController : Controller
    {
        private static readonly List<Auditoria> auditorias = new()
    {
        new Auditoria
        {
            IdAditoria = 1,
            DetalleAuditoria = "Validación exitosa de la transacción.",
            FechaAuditoria = DateTime.Now,
            Ubicacion = "Ciudad Central",
            IdTransaccion = 1001
        }
    };

        public IActionResult Index()
        {
            return View(auditorias);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Agregar(Auditoria nuevaAuditoria)
        {
            if (ModelState.IsValid)
            {
                nuevaAuditoria.IdAditoria = auditorias.Count + 1;
                auditorias.Add(nuevaAuditoria);
                return RedirectToAction("Index");
            }
            return View(nuevaAuditoria);
        }
    }
}
