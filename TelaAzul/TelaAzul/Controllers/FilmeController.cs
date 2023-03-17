using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class FilmeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Salvar(FilmeModel model)
        {
            try
            {

            } catch(Exception ex)
            {

            }
            return View("Cadastro");
        }
    }
}
