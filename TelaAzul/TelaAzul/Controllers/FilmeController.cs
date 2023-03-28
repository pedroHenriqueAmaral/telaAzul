using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class FilmeController : Controller
    {
        /*
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Cadastro()
        {
            return  View(new FilmeModel());
        }
        */

        public IActionResult Cadastro()
        {
            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Nome
            });
            return View(new FilmeModel());
        }

        [HttpPost]
        public IActionResult Salvar(FilmeModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    FilmeModel filmodel = new FilmeModel();
                    filmodel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Salvo com sucesso ";
                } catch (Exception ex) 
                {
                    ViewBag.classe = "alert-danger";
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                }
            } else
            {
                ViewBag.classe = "alert-danger";
                ViewBag.classe = "Erro: Confira os campos do formulário. ";
            }
            
            // Colocar os gêneros em um dropdown no cadastro do filme
            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Nome
            });

            return View("cadastro", model);
        }

        public IActionResult Listar()
        {
            FilmeModel filmodel = new FilmeModel();
            List<FilmeModel> lista = filmodel.Listar();

            return View(lista);
        }

        public IActionResult PreAlterar(int id)
        {
            FilmeModel filmodel = new FilmeModel();

            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(g => new SelectListItem()
            {
                Value = g.Id.ToString(),
                Text = g.Nome
            });

            return View("cadastro", filmodel.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            FilmeModel filmodel = new FilmeModel();

            try
            {
                filmodel.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = "Excluido com sucesso.";
            } catch (Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }
            return View("listar", filmodel.Listar());
        }
    }
}
