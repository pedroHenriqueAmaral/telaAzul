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

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(20, ErrorMessage = "Máximo 20 Caractéres")]
        public String ? Nome { get; set; }

        // public virtual ICollection<Filme>? Filme { get; set; }

        public GeneroModel Salvar(GeneroModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Genero genero = mapper.Map<Genero>(model);

            using var contexto = new Context();
            var repo = new GeneroRepo(contexto);

            if(model.Id == 0)
                repo.Inserir(genero);
            else
                repo.Alterar(genero);

            contexto.SaveChanges();
            
            model.Id = genero.Id;
            return model;
        }

        public List<GeneroModel> Listar()
        {
            List<GeneroModel> ? listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(var contexto = new Context())
            {
                var repo = new GeneroRepo(contexto);
                List<Genero> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<GeneroModel>>(lista);
            }
            return listamodel;
        }

        public GeneroModel Selecionar(int id)
        {
            GeneroModel ? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using var contexto = new Context();
            var repo = new GeneroRepo(contexto);
            Genero genero = repo.Recuperar(g => g.Id == id);
            model = mapper.Map<GeneroModel>(genero);
            
            return model; 
        }

        public void Excluir(int id)
        {
            using var contexto = new Context();
            var repo = new GeneroRepo(contexto);
            Genero genero = repo.Recuperar(g => g.Id == id);
            repo.Excluir(genero);
            contexto.SaveChanges();
        }
    }
}
