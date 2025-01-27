using DiseñoArquitecturaProyecto.Models.DB;
using Microsoft.AspNetCore.Mvc;

namespace DiseñoArquitecturaProyecto.Controllers
{
    public class ReporteController : Controller
    {
        private static readonly List<Reporte> reportes = new()
    {
        new Reporte
        {
            IdReporte = 1,
            FechaReporte = DateTime.Now,
            Contenido = "Reporte inicial del sistema.",
            IdUsuario = 1
        }
    };

        public IActionResult Index()
        {
            return View(reportes);
        }

        public IActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(Reporte nuevoReporte)
        {
            if (ModelState.IsValid)
            {
                nuevoReporte.IdReporte = reportes.Count + 1;
                reportes.Add(nuevoReporte);
                return RedirectToAction("Index");
            }
            return View(nuevoReporte);
        }
    }
}
