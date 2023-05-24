using Microsoft.AspNetCore.Mvc;
using TelaAzul.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TelaAzul.Models;
using Entidades;

namespace TelaAzul.Controllers
{
    public class CompraController : Controller
    {
        public IActionResult Index()
        {
            List<ComprasFilmesModel> lista = new List<ComprasFilmesModel>();
            return View(lista);
        }

        public IActionResult excluirProduto(int id)
        {
            (new ComprasFilmesModel()).Excluir(id);
            var lista = (new ComprasFilmesModel()).Listar(HttpContext.Session.GetInt32("compraId").Value);
            return View("Index", lista);
        }

        public IActionResult Finalizar()
        {
            int compraId = HttpContext.Session.GetInt32("compraId").Value;
            CompraModel compra = new CompraModel().Selecionar(compraId);
            compra.StatusId = 3; // aguardando pagamento ( Id cadastrado no banco )
            var produtos = (new ComprasFilmesModel()).Listar(compraId);
            
            decimal total = 0;
            
            foreach (var item in produtos)
            {
                total += item.Valor;
            }
            
            compra.Valor = total;
            compra.ClienteId = HttpContext.Session.GetInt32("clienteId").Value;

            //gerar o pagamento
            ClienteModel cliente = new ClienteModel().Selecionar(HttpContext.Session.GetInt32("clienteId").Value);
            
            Task<RetornoMercadoPago> retorno = new CompraModel().GerarPagamentoMercadoPago(new MercadoPagoModel()
            {
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cidade = cliente.Cidade,
                Estado = cliente.Estado,
                Cep = cliente.Cep,   
                Logradouro = cliente.Logradouro,
                PagamentoId = compraId,
                Valor = total,
                NomePlano = "Venda Ingresso",
            });

            /*
            Como não tenho cliente aqui vou fazer fixo para testar.
            Task<RetornoMercadoPago> retorno = new ComprasModel().gerarPagamentoMercadoPago(new MercadoPagoModel()
            {
                email = "meuamigovet@gmail.com",
                cidade = "Presidente Prudente",
                cep = "19025563",
                estado = "SP",
                idPagamento = idCompra,
                logradouro = "Avenida Brasil",
                nome = "Cliente teste",
                nomePlano = "Venda Ingresso Toledo",
                numero = "20",
                telefone = "1897865695",
                valor = total
            });
            */

            if (retorno.Result.Status == "Sucesso")
            {
                compra.PreferenciaId = retorno.Result.PreferenciaId;
                compra.Url = retorno.Result.Url;
            }
            else
            {
                compra.StatusId = 4; //cancelada
            }

            new CompraModel().Salvar(compra);
            return Redirect(retorno.Result.Url);
        }

        public virtual JsonResult AlterarQtde(int id, int quantidade)
        {
            ComprasFilmesModel produto = (new ComprasFilmesModel()).Selecionar(id);
            produto.Quantidade = quantidade;
            
            decimal valorUnitario = new FilmeModel().Selecionar(produto.FilmeId).Valor;
            produto.Valor = quantidade * valorUnitario;
            
            (new ComprasFilmesModel()).Salvar(produto);
            return new JsonResult(produto);
        }

        public IActionResult Finalizacao()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RetornoMercadoPago(string collectionId, string collectionStatus, string paymentId,
            string status, string externalReference, string paymentType, string merchantOrderId,
            string preferenceId, string siteId, string processingMode, string merchant_accountId)
        {
            CompraModel compra = new CompraModel().SelecionarIdPreferencia(preferenceId);
            compra.StatusId = 2; // Finalizado
            
            // compras.datapagamento = datetime.now;
            new CompraModel().Salvar(compra);

            if (status == "approved")
            {
                //baixar como pago
                return RedirectToAction("Finalizacao", "Compras");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ComprarIngresso(int id)
        {
            // Busca o valor 
            var ingresso = (new FilmeModel()).Selecionar(id);
            var valor = ingresso.Valor;

            // Valida se a compra não foi iniciada
            if (HttpContext.Session.GetInt32("compraId") == null)
            {
                // Insere na tabela de Compra
                var compra = new CompraModel()
                {
                    StatusId = 1,
                    DataCadastro = DateTime.Now,
                    Valor = valor
                };

                (new CompraModel()).Salvar(compra);
               
                // Insere na Sessão
                HttpContext.Session.SetInt32("compraId", compra.Id);
            }

            // Insere na tabela de ComprasFilmes
            var comprasFilmes = new ComprasFilmesModel()
            {
                FilmeId = id,
                CompraId = HttpContext.Session.GetInt32("compraId").Value,
                Valor = valor,
                Quantidade = 1
            };

            (new ComprasFilmesModel()).Salvar(comprasFilmes);

            var lista = (new ComprasFilmesModel()).Listar(HttpContext.Session.GetInt32("compraId").Value);
            return View("Index", lista);
        }
    }
}
