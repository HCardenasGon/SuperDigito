using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Login(ML.Usuario usuario, string password, string confirmPassword)
        {
            var bcrypt = new Rfc2898DeriveBytes(password, new byte[0], 10000, HashAlgorithmName.SHA256);
            // Obtener el hash resultante para la contraseña ingresada 
            var passwordHash = bcrypt.GetBytes(20);

            if (confirmPassword != null)
            {
                // Insertar usuario en la base de datos
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.Add(usuario);
                return View();
            }
            else
            {
                // Proceso de login
                usuario.Password = passwordHash;
                ML.Result result = BL.Usuario.GetByUsername(usuario.Username, usuario.Password);
                usuario = (ML.Usuario)result.Object;

                if (usuario.Password.SequenceEqual(passwordHash))
                {
                    return RedirectToAction("GetAll", "Historial", new {idUsuario = usuario.IdUsuario});
                }
            }
            return View();
        }
    }
}
