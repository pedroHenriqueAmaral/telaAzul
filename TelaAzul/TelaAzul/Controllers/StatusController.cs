using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Cadastro()
        {
            return View(new StatusController());
        }

        [HttpPost]
        public IActionResult Salvar(StatusModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var statusModel = new StatusModel();
                    statusModel.Salvar(model);

                    ViewBag.classe = "alert-success";
                    ViewBag.msg = " Status " + model.Id + " cadastrado com sucesso. ";
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
            StatusModel model = new();
            List<StatusModel> lista = model.Listar();
            return View(lista);
        }

        public IActionResult PreAlterar(int id)
        {
            StatusModel statusModel = new();

            List<StatusModel> lista = (new StatusModel()).Listar();
            ViewBag.listaStatus = lista.Select(status => new SelectListItem()
            {
                Value = status.Id.ToString(),
                Text = status.Descricao
            });

            return View("Cadastro", statusModel.Selecionar(id));
        }

        public IActionResult Excluir(int id)
        {
            var model = new StatusModel();
            try
            {
                model.Excluir(id);

                ViewBag.classe = "alert-success";
                ViewBag.msg = "Status Id: " + model.Id + " excluído com sucesso. ";
            }
            catch (Exception ex)
            {
                ViewBag.classe = "alert-danger";
                ViewBag.msg = "Erro: " + ex.Message;
            }
            return View("Listar", model.Listar());
        }
    }
}
