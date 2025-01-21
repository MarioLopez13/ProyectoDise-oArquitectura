using Microsoft.AspNetCore.Mvc;
using DiseñoArquitecturaProyecto.Models.DB;
namespace DiseñoArquitecturaProyecto.Controllers
{
    public class Login : Controller
    {
        //conexion base de datos
        static string conn = "Data Source=CASAALEX;initial catalog=Proyect;Integrated Security=True";
        public IActionResult Acceso()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }
        /*[HttpPost]
        public IActionResult Registrar(Cliente newCliente)
        {
            bool registrado;
            string mensaje;
            if (newCliente.c )
            {
            
            }
            return View();
        }*/
    }
}
