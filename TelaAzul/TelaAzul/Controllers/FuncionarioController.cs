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
                    FuncionarioModel funcModel = new();
                    funcModel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Funcionáio " + model.Nome + " cadastrado com sucesso!";
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

        public IActionResult Listar()
        {
            FuncionarioModel model = new();
            List<FuncionarioModel> lista = model.Listar();
            return View(lista);
        }

        public IActionResult PreAlterar(int id)
        {
            FuncionarioModel model = new ();
            return View("Cadastro", model.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            FuncionarioModel model = new();

            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = "Funcionário " + model.Nome + " excluido com sucesso.";
            }
            catch (Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }

            return View("Listar", model.Listar());
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
