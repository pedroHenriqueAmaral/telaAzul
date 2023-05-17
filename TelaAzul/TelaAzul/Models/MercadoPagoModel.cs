using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace TelaAzul.Models
{
    public class MercadoPagoModel
    { 
        public String email { get; set; }
        public String nome { get; set; }
        public String cidade { get; set; }
        public String estado { get; set; }
        public String telefone { get; set; }
        public int idPagamento { get; set; }
        public String logradouro { get; set; }
        public String numero { get; set; }
        public String cep { get; set; }
        public String nomePlano { get; set; }
        public decimal valor { get; set; }

        public async Task<RetornoMercadoPago> GerarPagamentoMercadoPago(MercadoPagoModel model)
        {

            RetornoMercadoPago ret = new RetornoMercadoPago();
            try
            {

                string cidade = model.cidade;
                string estado = model.estado;



                // Adicione as credenciais
                MercadoPagoConfig.AccessToken = "CREDENCIAIS";


                String[] split = model.nome.Split(' ');
                // ...
                var payer = new PreferencePayerRequest
                {
                    Name = split[0],
                    Surname = split[split.Length - 1],
                    Email = model.email,
                    Phone = new PhoneRequest
                    {
                        AreaCode = "",
                        Number = model.telefone,
                    },

                    Identification = new IdentificationRequest
                    {
                        Type = "DNI",
                        Number = model.idPagamento.ToString(),
                    },

                    Address = new AddressRequest
                    {
                        StreetName = model.logradouro,
                        StreetNumber = model.numero,
                        ZipCode = model.cep,
                    },
                };
                // ...


                // ...
                var item = new PreferenceItemRequest
                {
                    Id = model.idPagamento.ToString(),
                    Title = model.descricao,
                    Description = model.descricao,
                    CategoryId = "CATEGORIA",
                    Quantity = 1,
                    CurrencyId = "BRL",
                    UnitPrice = model.valor,
                };
                // ...


                var request = new PreferenceRequest
                {
                    // ...
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        Success = "ENDPOINT_Retorno_sucesso",
                        Failure = "ENDPOINT_Retorno_falha",
                        Pending = "ENDPOINT_Retorno_pendencias",
                    },
                    AutoReturn = "approved",
                    Payer = payer,
                    Items = new List<PreferenceItemRequest>()
                };
                request.Items.Add(item);

                // Cria a preferência usando o client
                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                ret.url = preference.InitPoint;
                ret.idPreferencia = preference.Id;
                ret.status = "SUCESSO";
                // preference.
                return ret;

            }
            catch (Exception ex)
            {
                ret.status = "ERRO";
                ret.erro = ex.Message;
                return ret;
            }
        }
    }



}



}
