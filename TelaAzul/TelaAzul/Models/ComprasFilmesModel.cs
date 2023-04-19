using AutoMapper;
using Contexto;
using Entidades;
using Repositorio;

namespace TelaAzul.Models
{
    public class ComprasFilmesModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Decimal Valor { get; set; }
        public int CompraId { get; set; }
        public int FilmeId { get; set; }
        public virtual FilmeModel ? Filme { get; set; }

        public ComprasFilmesModel Selecionar(int id)
        {
            ComprasFilmesModel? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(var contexto = new Context())
            {
                var repo = new ComprasFilmesRepo(contexto);

                ComprasFilmes comprasFilmes = repo.Recuperar(c => c.Id == id);
                model = mapper.Map<ComprasFilmesModel>(comprasFilmes);
            }
            return model;
        }

        public ComprasFilmesModel Salvar(ComprasFilmesModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            ComprasFilmes comprasFilmes = mapper.Map<ComprasFilmes>(model);

            using(var contexto = new Context())
            {
                var repo = new ComprasFilmesRepo(contexto);

                if (model.Id == 0)
                    repo.Inserir(comprasFilmes);
                else
                    repo.Alterar(comprasFilmes);

                contexto.SaveChanges();
            }
            model.Id = comprasFilmes.Id;
            return model;
        }

        public List<ComprasFilmesModel> Listar(int compraId)
        {
            List<ComprasFilmesModel> ? listaModel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            
            using(var contexto = new Context())
            {
                var repo = new ComprasFilmesRepo(contexto);
                List<ComprasFilmes> lista = repo.Listar(c => c.CompraId == compraId);
                listaModel = mapper.Map<List<ComprasFilmesModel>>(lista);
                
                foreach(var item in listaModel)
                {
                    item.Filme = new FilmeModel().Selecionar(item.FilmeId);
                }
            }
            return listaModel;
        }

        public void Excluir(int id)
        {
            using(var contexto = new Context())
            {
                var repo = new ComprasFilmesRepo(contexto);
                ComprasFilmes comprasFilmes = repo.Recuperar(c => c.Id == id);
                repo.Excluir(comprasFilmes);
                contexto.SaveChanges();
            }
        }
    }
}
