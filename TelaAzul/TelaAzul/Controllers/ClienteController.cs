using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Cadastro()
        {
            return View(new ClienteModel());
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(string email, string senha)
        {
            ClienteModel model = (new ClienteModel()).ValidaLogin(email, senha);

            if(model == null)
            {
                ViewBag.msg = "Dados incorretos ou inexistentes. ";
                ViewBag.classe = "alert-danger";
                
                return View("login");
            } 
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Salvar(ClienteModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ClienteModel climodel = new ClienteModel();
                    climodel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Cadastrado com sucesso. ";
                } catch (Exception ex)
                {
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                    ViewBag.classe = "alert-danger" ;
                }
            } 
            else
            {
                ViewBag.msg = "Erro: Verifique os campos. ";
                ViewBag.classe = "alert-danger";
            }
            return View("cadastro", model);
        }
    }
}
