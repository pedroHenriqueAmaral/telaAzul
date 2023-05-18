using AutoMapper;
using Repositorio;
using Contexto;
using Entidades;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class StatusModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        [Required(ErrorMessage = "Status inválido")]
        public String ? Descricao { get; set; }
        public int IdCompra { get; set; }
        public Compra ? Compra { get; set; }

        public StatusModel Salvar(StatusModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Status status = mapper.Map<Status>(model);

            using (Context contexto = new ())
            {
                StatusRepo repo = new (contexto);

                if (model.Id == 0)
                    repo.Inserir(status);
                else
                    repo.Alterar(status);

                contexto.SaveChanges();
            }
            model.Id = status.Id;
            return model;
        }

        public List<StatusModel> Listar()
        {
            List<StatusModel> ? listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new ())
            {
                StatusRepo repo = new (contexto);
                List<Status> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<StatusModel>>(lista);
            }
            return listamodel;
        }

        public StatusModel Selecionar(int id)
        {
            StatusModel ? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new ())
            {
                StatusRepo repo = new (contexto);
                Status status = repo.Recuperar(f => f.Id == id);
                model = mapper.Map<StatusModel>(status);
            }
            return model;
        }

        public void Excluir(int id)
        {
            using (Context contexto = new ())
            {
                StatusRepo repo = new (contexto);
                Status status = repo.Recuperar(f => f.Id == id);
                repo.Excluir(status);
                contexto.SaveChanges();
            }
        }
    }
}
