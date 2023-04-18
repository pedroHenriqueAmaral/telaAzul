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

        [HttpPost]
        public IActionResult Salvar(ClienteModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClienteModel cliModel = new ClienteModel();
                    cliModel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = model.Nome + " salvo com sucesso ";
                }
                catch (Exception ex)
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

        [HttpPost]
        public IActionResult Cadastrar(ClienteModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ClienteModel cliModel = new ClienteModel();
                    cliModel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = "Conta criada com sucesso ";
                }
                catch (Exception ex)
                {
                    ViewBag.classe = "alert.danger";
                    ViewBag.msg = "Erro: " + ex.Message + " | " + ex.InnerException;
                }
            }
            else
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro, verifique se os campos foram preenchidos corretamente ";
            }
            return View("Cadastro", model);
        }

        public IActionResult Listar()
        {
            ClienteModel model = new ClienteModel();
            List<ClienteModel> lista = model.Listar();
            return View(lista);
        }

        public IActionResult PreAlterar(int id) 
        {
            ClienteModel model = new ClienteModel();
            return View("Cadastro", model.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            ClienteModel model = new ClienteModel();

            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = model.Nome + " excluido com sucesso.";
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
        public IActionResult Logar(string email, string senha)
        {
            ClienteModel model = (new ClienteModel()).ValidaLogin(email, senha);

            if (model == null)
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
            // Limpa a Sessão
            HttpContext.Session.Remove("idCliente");
            HttpContext.Session.Remove("nomeCliente");
            HttpContext.Session.Clear();

            // Manda pra login
            return RedirectToAction("Login", "Cliente");
        }
    }
}
