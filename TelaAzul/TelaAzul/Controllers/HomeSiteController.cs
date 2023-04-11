using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class HomeSiteController : Controller
    {
        public IActionResult Index()
        {
            FilmeModel model = new FilmeModel();
            return View(model.Listar());
        }

        public IActionResult Detalhes(int id)
        {
            FilmeModel model = new FilmeModel();
            FilmeModel produto = model.Selecionar(id);
            return View(produto);
        }
    }
}
