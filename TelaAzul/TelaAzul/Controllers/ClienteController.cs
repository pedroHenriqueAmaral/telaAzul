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
                HttpContext.Session.SetInt32("idCliente", model.Id);
                HttpContext.Session.SetString("nomeCliente", model.Nome);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Sair()
        {
            /* Limpa a Sessão */
            HttpContext.Session.Remove("idCliente");
            HttpContext.Session.Remove("nomeCliente");
            HttpContext.Session.Clear();

            // Manda pra login
            return RedirectToAction("Login", "Cliente");
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
