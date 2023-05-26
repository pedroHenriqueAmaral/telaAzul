using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class FilmeController : Controller
    {
        // Upload de Imagem
        private readonly IWebHostEnvironment ? webHostEnvironment;
        public FilmeController(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Cadastro()
        {
            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(gen => new SelectListItem()
            {
                Value = gen.Id.ToString(),
                Text = gen.Nome
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
                    var filmodel = new FilmeModel();
                    filmodel.Salvar(model, webHostEnvironment);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Filme " + model.Titulo + " cadastrado com sucesso!";
                } 
                catch (Exception ex) 
                {
                    ViewBag.classe = "alert-danger";
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                }
            } 
            else
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: Confira os campos do formulário. ";
            }

            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(gen => new SelectListItem()
            {
                Value = gen.Id.ToString(),
                Text = gen.Nome
            });

            return View("Cadastro", model);
        }

        public IActionResult Listar()
        {
            FilmeModel filmodel = new ();
            List<FilmeModel> lista = filmodel.Listar();

            GeneroModel generoModel = new ();
            
            foreach(var item in lista)
            {
                item.Genero = generoModel.Selecionar(item.GeneroId);
            }
            return View(lista);
        }

        public IActionResult PreAlterar(int id)
        {
            var filmodel = new FilmeModel();

            List<GeneroModel> lista = (new GeneroModel()).Listar();
            ViewBag.listaGeneros = lista.Select(generos => new SelectListItem()
            {
                Value = generos.Id.ToString(),
                Text = generos.Nome
            });

            return View("Cadastro", filmodel.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            FilmeModel model = new ();

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
            
            return View("Listar", model.Listar());
        }
    }
}
