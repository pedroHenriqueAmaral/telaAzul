using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class GeneroController : Controller
    {
        public IActionResult Index()
        {
            // /Controller/Action -> Controller retorna View do mesmo nome da Action se não tiver definido no parâmetro.
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Salvar(FilmeModel model)
        {
            return View(model);
        }

        /*
         * 
        public IActionResult Exibe(String nome)
        {
            ViewBag.msg = nome; // Escreva na tela no local da ViewBag da View 
            return View("Index");
        }
        *
        [HttpPost] // Somente por method POST 
        public IActionResult Envia(CategoriaModel model) 
        {
            return View(model);
        }
        *
        */
    }
}
