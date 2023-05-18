using Microsoft.AspNetCore.Mvc;
using Projeto2023_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TelaAzul.Models;

namespace TelaAzul.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            List<ComprasFilmesModel> lista = new();
            return View(lista);
        }

        public IActionResult ExcluirFilme(int id)
        {
            var idCompra = HttpContext.Session.GetInt32("idCompra").Value;

            (new ComprasFilmesModel()).Excluir(id);
            var lista = (new ComprasFilmesModel()).Listar(idCompra);

            return View("Index", lista);
        }

        public IActionResult Finalizar()
        {
            int idCompra = HttpContext.Session.GetInt32("idCompra").Value;

            CompraModel compra = new CompraModel().Selecionar(idCompra);
            compra.idStatus = 3; // status para Aguardando Pagamento (cadastrado no banco)

            var produtos = (new ComprasFilmesModel()).Listar(idCompra);
            decimal total = 0;

            foreach (var item in produtos)
            {
                total += item.Valor;
            }
            compra.Valor = total;
            
            // Gera pagamento
            
            /*   
             * ClienteModel cliente = new ClienteModel().selecionar(HttpContext.Session.GetInt32("idCliente").Value);
               Task<RetornoMercadoPago> retorno = new ComprasModel().gerarPagamentoMercadoPago(new MercadoPagoModel()
               {
                   email = cliente.email,
                   cidade = cliente.cidade,
                   cep = cliente.cep,
                   estado = cliente.estado,
                   idPagamento = idCompra,
                   logradouro = cliente.logradouro,
                   nome = cliente.nome,
                   nomePlano = "Venda Ingresso Toledo",
                   numero = cliente.numero,
                   telefone = cliente.telefone,
                   valor = total
               });
            */
            
            // como não tem cliente aqui, fixo para testar
            Task<RetornoMercadoPago> retorno = new CompraModel().GerarPagamentoMercadoPago(new MercadoPagoModel()
            {
                IdPagamento = idCompra,
                Nome = "Cliente teste",
                Email = "meuamigovet@gmail.com",
                NomePlano = "Venda Ingresso Toledo",
                Cidade = "Presidente Prudente",
                Cep = "19025563",
                Estado = "SP",   
                Logradouro = "Avenida Brasil",
                Numero = "20",
                Telefone = "1897865695",
                Valor = total
            });

            if (retorno.Result.Status == "Sucesso")
            {
                compra.IdPreferencia = retorno.Result.IdPreferencia;
                compra.Url = retorno.Result.Url;
            }
            else
            {
                compra.IdStatus = 4; // Compra cancelada
            }

            new CompraModel().Salvar(compra);
            return Redirect(retorno.Result.Url);
        }

        public virtual JsonResult alterarQtde(int id, int qtde)
        {
            ComprasProdutoModel produto =
                (new ComprasProdutoModel()).selecionar(id);
            produto.qtde = qtde;
            decimal valorUnitario = new ProdutoModel().selecionar(produto.idProduto).valor;
            produto.valor = qtde * valorUnitario;
            (new ComprasProdutoModel()).salvar(produto);
            return new JsonResult(produto);

        }

        public IActionResult Finalizacao()
        {

            return View();
        }

        [HttpGet]
        public IActionResult retornoMercadoPago(string collection_id, string collection_status, string payment_id,
       string status, string external_reference, string payment_type, string merchant_order_id,
       string preference_id, string site_id, string processing_mode, string merchant_account_id)
        {
            //obter o pagamento pelo idPreferencia(preference_id);
            ComprasModel compras = new ComprasModel().selecionarIdPreferencia(
                                                        preference_id);
            compras.idStatus = 2;//finalizado
            //compras.datapagamento = datetime.now;
            new ComprasModel().salvar(compras);

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


        //parametro id = id_produto
        public IActionResult comprarProduto(int id)
        {

            //buscar o valor do produto
            var produto = (new ProdutoModel()).selecionar(id);
            var valor = produto.valor;

            //validar se a compra não iniciou
            if (HttpContext.Session.GetInt32("idCompra") == null)
            {


                //inserir na tabela de compras
                var compras = new ComprasModel()
                {
                    dataCadastro = DateTime.Now,
                    idStatus = 1,
                    valor = valor
                };

                (new ComprasModel()).salvar(compras);
                //inserir na sessão
                HttpContext.Session.SetInt32("idCompra", compras.id);

            }
            //inserir na tabela de compras produtos
            var prod = new ComprasProdutoModel()
            {
                idCompra = HttpContext.Session.GetInt32("idCompra").Value,
                idProduto = id,//parametro
                qtde = 1,
                valor = valor
            };
            (new ComprasProdutoModel()).salvar(prod);

            var lista = (new ComprasProdutoModel()).listar(
                HttpContext.Session.GetInt32("idCompra").Value);

            return View("Index", lista);
        }
    }
}
