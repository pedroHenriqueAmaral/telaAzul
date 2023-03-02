using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View(); // /Categoria/Index -> Controller retorna View do mesmo nome da Action
        }

        public IActionResult Exibe(String nome)
        {
            ViewBag.msg = nome; /* Escreva na tela no local da ViewBag da View */
            return View("Index");
        }

        /*
        [HttpPost] // Somente por method POST 
        public IActionResult Envia(CategoriaModel model) 
        {
            return View(model);
        }
        */
    }
}
