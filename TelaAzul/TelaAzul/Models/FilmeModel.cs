using AutoMapper;
using Contexto;
using Entidades;
using Repositorio;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class FilmeModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Informe o Título do Filme")]
        [MinLength(2, ErrorMessage = "No mínimo 2 caractéres")]
        [MaxLength(50, ErrorMessage = "No máximo 50 caractéres")]
        public string ? Titulo { get; set; }

        [Display(Name = "Sinopse")]
        [Required(ErrorMessage = "Informe a Sinopse do filme")]
        [MinLength(5, ErrorMessage = "No mínimo 5 caractéres")]
        [MaxLength(250, ErrorMessage = "Máximo 250 Caractéres")]
        public string ? Sinopse { get; set; }

        public string ? Imagem { get; set; }
        [Display(Name = "Imagem de Cartaz")]
        // Imagem não é requerida por causa da função de Alterar o cadastro
        public IFormFile ? ArquivoImagem { get; set; }

        [Display(Name = "Duração do Filme")]
        [Required(ErrorMessage = "Informe a duração do filme em minutos")]
        public int ? Duracao { get; set; }

        [Display(Name = "Áudio")]
        [Required(ErrorMessage = "Selecione uma opção válida")]
        public string ? Audio { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Selecione uma opção válida")]
        public Decimal Valor { get; set; }

        [Display(Name = "Gênero do Filme")]
        // [Required(ErrorMessage = "Informe um gênero cadastrado")]
        public int GeneroId { get; set; }
        public GeneroModel ? Genero { get; set; }

        public FilmeModel Salvar(FilmeModel model, IWebHostEnvironment webHostEnvironment)
        {
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());
            Filme filme = mapper.Map<Filme>(model);

            // Validação do Upload de Imagem
            if(model.ArquivoImagem != null)
                filme.Imagem = Upload(model.ArquivoImagem, webHostEnvironment);
            else { 
                if (model.Id != 0) // alterando o filme já cadastrado
                    filme.Imagem  = Selecionar(model.Id).Imagem;
            }

            using(Context contexto = new())
            {
                FilmeRepo repo = new(contexto);

                if (model.Id == 0)
                    repo.Inserir(filme);
                else
                    repo.Alterar(filme);
                
                contexto.SaveChanges();
            }
            model.Id = filme.Id;
            return model;
        }

        public List<FilmeModel> Listar()
        {
            List<FilmeModel> ? listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(Context contexto = new())
            {
                FilmeRepo repo = new(contexto);
                List<Filme> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<FilmeModel>>(lista);
            }
            return listamodel;
        }

        public FilmeModel Selecionar(int id)
        {
            FilmeModel ? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new())
            {
                FilmeRepo repo = new(contexto);
                Filme filme = repo.Recuperar(f => f.Id == id);
                model = mapper.Map<FilmeModel>(filme);
            }
            return model;
        }

        public void Excluir(int id)
        {
            using (Context contexto = new())
            {
                FilmeRepo repo = new(contexto);
                Filme filme = repo.Recuperar(f => f.Id == id);
                repo.Excluir(filme);
                
                contexto.SaveChanges();
            }
        }

        private String Upload(IFormFile arquivoImagem, IWebHostEnvironment webHostEnvironment)
        {
            string ? nomeUnicoArquivo = null;

            if(arquivoImagem != null)
            {
                string pastaImagens = Path.Combine(webHostEnvironment.WebRootPath, "Imagens");
                nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + arquivoImagem.FileName;
                
                string caminhoArquivo = Path.Combine(pastaImagens, nomeUnicoArquivo);

                using(var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    arquivoImagem.CopyTo(fileStream);
                }
            }
            return nomeUnicoArquivo;
        }
    }
}
