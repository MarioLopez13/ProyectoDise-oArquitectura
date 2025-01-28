using Microsoft.AspNetCore.Mvc;
using DiseñoArquitecturaProyecto.Models.DB;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System;

namespace AppLogin.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBcontext _appDBcontext;

        public AccesoController(AppDBcontext appDbContext)
        {
            _appDBcontext = appDbContext;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrarse(UsuarioVM modelo)
        {
            if (modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                Nombres = modelo.NombreCompleto.Split(' ')[0],
                Apellidos = modelo.NombreCompleto.Contains(' ') ? modelo.NombreCompleto.Substring(modelo.NombreCompleto.IndexOf(' ') + 1) : string.Empty,
                Correo = modelo.Correo,
                Contrasea = modelo.Clave,
                Cedula = decimal.Parse(modelo.Cedula), // Assuming Cedula is provided as a string and needs conversion.
                Direccion = "", // Placeholder for address if not provided.
                Rol = "Usuario", // Default role.
                Estado = "Activo" // Default active state.
            };

            await _appDBcontext.Usuarios.AddAsync(usuario);
            await _appDBcontext.SaveChangesAsync();

            if (usuario.IdUsuario != 0)
            {
                // Log the registration event
                Log log = new Log()
                {
                    Evento = "Registro",
                    Detalle = $"Usuario {usuario.Nombres} {usuario.Apellidos} registrado con éxito.",
                    IdUsuario = usuario.IdUsuario
                };

                await _appDBcontext.Logs.AddAsync(log);
                await _appDBcontext.SaveChangesAsync();

                return RedirectToAction("Login", "Acceso");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? usuarioEncontrado = await _appDBcontext.Usuarios
                .Where(u => u.Correo == modelo.Correo && u.Contrasea == modelo.Clave)
                .FirstOrDefaultAsync();

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            // Create claims for the user
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{usuarioEncontrado.Nombres} {usuarioEncontrado.Apellidos}"),
                new Claim(ClaimTypes.Email, usuarioEncontrado.Correo),
                new Claim(ClaimTypes.Role, usuarioEncontrado.Rol)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            // Log the login event
            Log log = new Log()
            {
                Evento = "Inicio de sesión",
                Detalle = $"Usuario {usuarioEncontrado.Nombres} {usuarioEncontrado.Apellidos} inició sesión.",
                IdUsuario = usuarioEncontrado.IdUsuario
            };

            await _appDBcontext.Logs.AddAsync(log);
            await _appDBcontext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            // Log the logout event if the user is authenticated
            if (User.Identity!.IsAuthenticated)
            {
                var userId = decimal.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var usuario = await _appDBcontext.Usuarios.FindAsync(userId);

                if (usuario != null)
                {
                    Log log = new Log()
                    {
                        Evento = "Cierre de sesión",
                        Detalle = $"Usuario {usuario.Nombres} {usuario.Apellidos} cerró sesión.",
                        IdUsuario = usuario.IdUsuario
                    };

                    await _appDBcontext.Logs.AddAsync(log);
                    await _appDBcontext.SaveChangesAsync();
                }
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
    }
}
