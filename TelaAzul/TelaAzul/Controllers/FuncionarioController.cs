using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class FuncionarioController : Controller
    {
        public IActionResult Cadastro()
        {
            return View(new FuncionarioModel());
        }

        [HttpPost]
        public IActionResult Salvar(FuncionarioModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    FuncionarioModel climodel = new FuncionarioModel();
                    model.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = model.Nome + " cadastrado com sucesso ";
                }
                catch (Exception ex)
                {
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                    ViewBag.classe = "alert-danger";
                }
            }
            else
            {
                ViewBag.msg = "Erro: Verifique os campos ";
                ViewBag.classe = "alert-danger";
            }
            return View("Cadastro", model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar(String email, String senha)
        {
            FuncionarioModel model = (new FuncionarioModel()).ValidaLogin(email, senha);

            if (model == null)
            {
                ViewBag.msg = "Dados incorretos ou inexistentes. ";
                ViewBag.classe = "alert-danger";
                
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetInt32("idFuncionario", model.Id);
                HttpContext.Session.SetString("nomeFuncionario", model.Nome);
                
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("idFuncionario");
            HttpContext.Session.Remove("nomeFuncionario");
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Funcionario");
        }  
    }
}
