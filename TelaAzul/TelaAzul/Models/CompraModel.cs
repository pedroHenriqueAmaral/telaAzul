using AutoMapper;
using Contexto;
using Entidades;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using TelaAzul.Models;
using Repositorio;

namespace TelaAzul.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataCadastro { get; set; }
        public Decimal Valor { get; set; }
        public String ? PreferenciaId { get; set; }
        public String ? Url { get; set; }

        public CompraModel Salvar(CompraModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Compra compra = mapper.Map<Compra>(model);

            using(var contexto = new Context())
            {
                var repo = new CompraRepo(contexto);

                if(model.Id == 0)
                    repo.Inserir(compra);
                else
                    repo.Alterar(compra);

                contexto.SaveChanges();
            }
            model.Id = compra.Id;
            return model;
        }

        public CompraModel Selecionar(int id)
        {
            CompraModel ? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using var contexto = new Context();
            var repo = new CompraRepo(contexto);
            Compra compra = repo.Recuperar(g => g.Id == id);
            model = mapper.Map<CompraModel>(compra);

            return model;
        }

        public CompraModel SelecionarIdPreferencia(String idPreferencia)
        {
            CompraModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new())
            {
                CompraRepo repositorio =new(contexto);
                Compra preferencia = repositorio.Recuperar(c => c.IdPreferencia == idPreferencia);
                model = mapper.Map<CompraModel>(preferencia);
            }

            return model;
        }

        public async Task<RetornoMercadoPago> GerarPagamentoMercadoPago(MercadoPagoModel model)
        {
            RetornoMercadoPago ret = new RetornoMercadoPago();
           
            try
            {
                string cidade = model.Cidade;
                string estado = model.Estado;

                // Adiciona credenciais
                MercadoPagoConfig.AccessToken = "";

                String[] split = model.Nome.Split(' ');
                
                var payer = new PreferencePayerRequest
                {
                    Name = split[0],
                    Surname = split[split.Length - 1],
                    Email = model.Email,
                    Phone = new PhoneRequest
                    {
                        AreaCode = "",
                        Number = model.Telefone,
                    },
                    Identification = new IdentificationRequest
                    {
                        Type = "DNI",
                        Number = model.PagamentoId.ToString(),
                    },
                    Address = new AddressRequest
                    {
                        StreetName = model.Logradouro,
                        StreetNumber = model.Numero,
                        ZipCode = model.Cep,
                    },
                };
                
                var item = new PreferenceItemRequest
                {
                    Id = model.PagamentoId.ToString(),
                    Title = "",
                    Description = "",
                    CategoryId = "Cinema",
                    Quantity = 1,
                    CurrencyId = "BRL",
                    UnitPrice = model.Valor,
                };

                var request = new PreferenceRequest
                {
                    BackUrls = new PreferenceBackUrlsRequest
                    {
                        /* link hospedado */
                        Success = "http://exemplo.com/Compras/retornoMercadoPago",
                        Failure = "ENDPOINT_Retorno_falha",
                        Pending = "ENDPOINT_Retorno_pendencias",
                    },
                    AutoReturn = "approved",
                    Payer = payer,
                    Items = new List<PreferenceItemRequest>()
                };
                request.Items.Add(item);

                var client = new PreferenceClient();
                Preference preference = await client.CreateAsync(request);
                ret.Url = preference.InitPoint;
                ret.PreferenciaId = preference.Id;
                ret.Status = "Sucesso";
                return ret;
            }
            catch (Exception ex)
            {
                ret.Status = "Erro";
                ret.Erro = ex.Message;
                return ret;
            }
        }
    }
}
