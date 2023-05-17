using AutoMapper;
using Contexto;
using Entidades;
using Repositorio;

namespace TelaAzul.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public Decimal Valor { get; set; }
        public int StatusId { get; set; }

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
    }
}
