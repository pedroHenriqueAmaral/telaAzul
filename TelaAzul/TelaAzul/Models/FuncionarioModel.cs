using AutoMapper;
using Repositorio;
using Contexto;
using Entidades;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        public String ? Nome { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime Data_Nascimento { get; set; }

        [Display(Name = "Data de Admissão")]
        public DateTime Data_Admissao{ get; set; }

        [Display(Name = "Cargo")]
        public String ? Cargo { get; set; }

        [Display(Name = "Salário Fixo")]
        public float Salario_Fixo { get; set; }

        [Display(Name = "E-mail")]
        public String ? Email { get; set; }

        [Display(Name = "Senha")]
        public String ? Senha { get; set; }

        public FuncionarioModel Salvar(FuncionarioModel model)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Funcionario func = mapper.Map<Funcionario>(model);

            using (Context contexto = new Context())
            {
                var repo = new FuncionarioRepo(contexto);

                if (model.Id == 0)
                    repo.Inserir(func);
                else
                    repo.Alterar(func);

                contexto.SaveChanges();
            }
            model.Id = func.Id;
            return model;
        }

        public List<FuncionarioModel> Listar()
        {
            List<FuncionarioModel> model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(Context contexto = new Context())
            {
                FuncionarioRepo repo = new FuncionarioRepo(contexto);
                List<Funcionario> lista = repo.ListarTodos();
                model = mapper.Map<List<FuncionarioModel>>(lista);
            }
            return model;
        }

        public FuncionarioModel Selecionar(int id)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            FuncionarioModel ? model = null;

            using(Context contexto = new Context())
            {
                FuncionarioRepo repo = new FuncionarioRepo(contexto);
                Funcionario func = repo.Recuperar(c => c.Id == id);

                model = mapper.Map<FuncionarioModel>(func);
            }
            return model;
        }

        public void Excluir(int id)
        {
            using (Context contexto = new Context())
            {
                FuncionarioRepo repo = new FuncionarioRepo(contexto);
                Funcionario func = repo.Recuperar(c => c.Id == id);

                repo.Excluir(func);
                contexto.SaveChanges();
            }
        }

        public FuncionarioModel ValidaLogin(String email, String senha)
        {
            FuncionarioModel? model = null;

            using (Context contexto = new Context())
            {
                var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

                FuncionarioRepo repo = new FuncionarioRepo(contexto);
                Funcionario func = repo.Recuperar(c => c.Email == email && c.Senha == senha);
                model = mapper.Map<FuncionarioModel>(func);
            }
            return model;
        }

    }
}
