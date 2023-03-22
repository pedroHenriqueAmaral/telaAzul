using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class GeneroController : Controller
    {
        /*
        public IActionResult Index()
        {
            // /Controller/Action -> Controller retorna View do mesmo nome da Action se não tiver definido no parâmetro.
            return View();
        }
        */

        public IActionResult Cadastro()
        {
            return View(new GeneroModel());
        }

        [HttpPost]
        public IActionResult Salvar(GeneroModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    GeneroModel genmodel = new GeneroModel();
                    genmodel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Salvo com sucesso ";

                } catch (Exception ex)
                {
                    ViewBag.classe = "alert.danger";
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                }
            }
            else
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro, verifique os campos";
            }

            return View("cadastro", model);
        }

        public IActionResult Listar()
        {
            GeneroModel genmodel = new GeneroModel();
            List<GeneroModel> lista = genmodel.Listar();
            return View(lista); 
        }

        public IActionResult PreAlterar(int id) // traz a tela do cadastro com um ID já cadastro para alterar
        {
            GeneroModel model = new GeneroModel();
            return View("cadastro", model.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            GeneroModel model = new GeneroModel();

            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = "Excluido com sucesso.";
            } catch(Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }
            return View("listar", model.Listar());
        }
    }
}
