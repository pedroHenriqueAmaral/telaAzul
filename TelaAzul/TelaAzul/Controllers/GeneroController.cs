using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class GeneroController : Controller
    {
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
                    var genmodel = new GeneroModel();
                    genmodel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Gênero " + model.Nome + " foi salvo com sucesso ";

                } 
                catch(Exception ex)
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

            return View("Cadastro", model);
        }

        public IActionResult Listar()
        {
            var genmodel = new GeneroModel();
            List<GeneroModel> lista = genmodel.Listar();
            return View(lista); 
        }

        // manda para tela do cadastro com o ID para alterar (!= 0)
        public IActionResult PreAlterar(int id) 
        {
            var model = new GeneroModel();
            return View("Cadastro", model.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            var model = new GeneroModel();

            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = "Gênero excluído com sucesso. ";
            } catch(Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }
            return View("Listar", model.Listar());
        }
    }
}
