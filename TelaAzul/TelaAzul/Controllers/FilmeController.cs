using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class FilmeController : Controller
    {
        // Para Upload de Imagem
        private readonly IWebHostEnvironment ? webHostEnvironment;
        public FilmeController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

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
                    filmodel.Salvar(model, webHostEnvironment);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = model.Titulo + " cadastrado com sucesso!";
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

            GeneroModel genm = new GeneroModel();
            foreach (var item in lista)
            {
                item.Genero = genm.Selecionar(item.GeneroId);
            }
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
            FilmeModel model = new FilmeModel();
            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = model.Titulo + " excluido com sucesso.";
            } catch (Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }
            
            return View("listar", model.Listar());
        }
    }
}
