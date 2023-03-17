using AutoMapper;
using Repositorio;
using Contexto;
using Entidades;

namespace TelaAzul.Models
{
    public class FilmeModel
    {
        public int Id { get; set; }
        public string ? Titulo { get; set; }
        public string ? Titulo_original { get; set; }
        public string ? Sinopse { get; set; }
        public string ? Duracao { get; set; }
        public string ? Audio { get; set; }

        public int IdGenero { get; set; }
        // virtual Venda

        public FilmeModel Salvar(FilmeModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            Filme filme = mapper.Map<Filme>(model);
            using (Context contexto = new Context())
            {
                FilmeRepo repo = new FilmeRepo(contexto);

                if (model.Id == 0)
                    repo.Inserir(filme);
                else
                    repo.Alterar(filme);
                
                contexto.SaveChanges();
            }

            model.Id = filme.Id;
            return model;
        }
    }
}
