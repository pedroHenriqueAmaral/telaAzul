using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            var lista = new List<ComprasFilmesModel>();
            return View(lista);
        }

        public IActionResult ComprarFilme(int filmeId)
        {
            // Buscar Valor Filme
            var filme = (new FilmeModel()).Selecionar(filmeId);
            var valor = filme.Valor;

            // Valida se a Sessão da Compra não está aberta
            if(HttpContext.Session.GetInt32("compraId") == null)
            {
                // Insere na Tabela de Compra
                var compra = new CompraModel()
                {
                    DataCadastro = DateTime.Now, // Data atual do Servidor
                    StatusId = 1,
                    Valor = valor 
                };
                (new CompraModel()).Salvar(compra);

                // Insere na Sessão
                HttpContext.Session.SetInt32("compraId", compra.Id);
            }

            // Insere na Tabela de ComprasFilmes
            var comprasFilmes = new ComprasFilmesModel()
            {
                CompraId = HttpContext.Session.GetInt32("compraId").Value,
                FilmeId = filmeId,
                Quantidade = 1,
                Valor = valor
            };
            (new ComprasFilmesModel()).Salvar(comprasFilmes);

            var lista = (new ComprasFilmesModel()).Listar(
                HttpContext.Session.GetInt32("compraId").Value);

            return View("Index", lista);
        }

        public IActionResult ExcluirFilme(int id)
        {
            (new ComprasFilmesModel()).Excluir(id);
            
            var lista = (new ComprasFilmesModel()).Listar(
                HttpContext.Session.GetInt32("compraId").Value);

            return View("Index", lista);
        }

        public virtual JsonResult AlterarQuantidade(int id, int quantidade)
        {
            var filme = (new ComprasFilmesModel()).Selecionar(id);
            filme.Quantidade = quantidade;
            Decimal valorUnitario = new FilmeModel().Selecionar(filme.FilmeId).Valor;
            filme.Valor = quantidade * valorUnitario;
            
            (new ComprasFilmesModel()).Salvar(filme);
            return new JsonResult(filme);
        }
    }
}
