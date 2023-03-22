using AutoMapper;
using Contexto;
using Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class GeneroModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(20, ErrorMessage = "Máximo 20 Caractéres")]
        public String ? Nome { get; set; }

        // virtual ICollection<Filme> ? Filmes {get;set;}

        public GeneroModel Salvar(GeneroModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Genero genero = mapper.Map<Genero>(model);

            using(Context contexto = new Context())
            {
                GeneroRepo repo = new GeneroRepo(contexto);

                if (model.Id == 0)
                    repo.Inserir(genero);
                else
                    repo.Alterar(genero);

                contexto.SaveChanges();
            }
            model.Id = genero.Id;
            return model;
        }

        public List<GeneroModel> Listar()
        {
            List<GeneroModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(Context contexto = new Context())
            {
                GeneroRepo repo = new GeneroRepo(contexto);
                List<Genero> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<GeneroModel>>(lista);
            }
            return listamodel;
        }

        public GeneroModel Selecionar(int id)
        {
            GeneroModel model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new Context())
            {
                GeneroRepo repo = new GeneroRepo(contexto);
                Genero gen = repo.Recuperar(g => g.Id == id);
                model = mapper.Map<GeneroModel>(gen);
            }
            return model;
        }

        public void Excluir(int id)
        {
            using (Context contexto = new Context())
            {
                GeneroRepo repo = new GeneroRepo(contexto);
                Genero genero = repo.Recuperar(g => g.Id == id);
                repo.Excluir(genero);
                contexto.SaveChanges();
            }
        }
    }
}
