using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HistorialController : Controller
    {
        [HttpGet]
        public IActionResult GetAll(int IdUsuario)
        {
            ML.Historial historial = new ML.Historial();
            historial.Usuario = new ML.Usuario();
            historial.Usuario.IdUsuario = IdUsuario;
            ML.Result result = BL.Historial.GetAll(historial.Usuario.IdUsuario);

            if (result.Correct)
            {
                historial.Historiales = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta Users";
            }

            return View(historial);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Historial historial)
        {
            ML.Result resultNumero = BL.Historial.Calcular(historial);
            historial = (ML.Historial)resultNumero.Object;
            if (resultNumero.Correct)
            {
                ML.Result resultFind= BL.Historial.findNumero(historial.Usuario.IdUsuario, historial.Numero);

                if (resultFind.Correct)
                {
                    ML.Result resultUpdate = BL.Historial.Update(historial);
                }
                else
                {
                    ML.Result resultAdd = BL.Historial.Add(historial);
                }
            }

            ML.Result result = BL.Historial.GetAll(historial.Usuario.IdUsuario);

            if (result.Correct)
            {
                historial.Historiales = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta Users";
            }

            return View(historial);
        }

        [HttpPost]
        public ActionResult Delete(ML.Historial historial)
        {
            ML.Result result = BL.Historial.Delete(historial);
            if (result.Correct)
            {
                ViewBag.Message = "Registro correctamente Eliminado";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al eliminar el registro";
            }
            return RedirectToAction("GetAll", "Historial", new { idUsuario = historial.Usuario.IdUsuario });
        }
    }
}
