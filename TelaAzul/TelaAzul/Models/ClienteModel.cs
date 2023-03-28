using AutoMapper;
using Contexto;
using Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class ClienteModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        public string ? Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime Data_Nascimento { get; set; }

        [Display(Name = "Tipo")]
        //[Required(ErrorMessage = "Campo Obrigatório")]
        public string ? Tipo_Cliente { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        public string ? Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(30, ErrorMessage = "Máximo 30 Caractéres")]
        public string ? Senha { get; set; }


        public ClienteModel ValidaLogin(string email, string senha)
        {
            ClienteModel ? model = null;

            using(Context contexto = new Context())
            {
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

                ClienteRepo repo = new ClienteRepo(contexto);
                Cliente cli = repo.Recuperar(c => c.Email == email && c.Senha == senha);

                model = mapper.Map<ClienteModel>(cli);
            }
            return model;
        }

        public ClienteModel Salvar(ClienteModel model) 
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Cliente cliente = mapper.Map<Cliente>(model);

            using(Context contexto = new Context())
            {
                ClienteRepo repo = new ClienteRepo(contexto);

                if (model.Id == 0)
                    repo.Inserir(cliente);
                else
                    repo.Alterar(cliente);

                contexto.SaveChanges();
            }
            model.Id = cliente.Id;
            return model;
        }

        public ClienteModel Selecionar(int id)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            ClienteModel ? model = null;
            
            using(Context contexto = new Context())
            {
                ClienteRepo repo = new ClienteRepo(contexto);
                Cliente cli = repo.Recuperar(c => c.Id == id);

                model = mapper.Map<ClienteModel>(cli);
            }
            return model;
        }

        public List<ClienteModel> Listar()
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            List<ClienteModel> ? listamodel = null;

            using(Context contexto = new Context())
            {
                ClienteRepo repo = new ClienteRepo(contexto);
                List<Cliente> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<ClienteModel>>(lista);
            }
            return listamodel;
        }

        public void Excluir(int id)
        {
            using(Context contexto = new Context())
            {
                ClienteRepo repo = new ClienteRepo(contexto);
                Cliente cliente = repo.Recuperar(c => c.Id == id);
                
                repo.Excluir(cliente);
                contexto.SaveChanges();
            }
        }
        
    }
}
