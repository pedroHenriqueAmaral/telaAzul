﻿using AutoMapper;
using Repositorio;
using Contexto;
using Entidades;
using System.ComponentModel.DataAnnotations;

namespace TelaAzul.Models
{
    public class FilmeModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        public string ? Titulo { get; set; }

        /*
        [Display(Name = "Título Original")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Máximo 50 Caractéres")]
        public string ? Titulo_original { get; set; }
        */

        [Display(Name = "Sinopse")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(250, ErrorMessage = "Máximo 250 Caractéres")]
        public string ? Sinopse { get; set; }

        [Display(Name = "Duração do Filme")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int ? Duracao { get; set; }

        [Display(Name = "Áudio")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string ? Audio { get; set; }

        [Display(Name = "Gênero")]
        public int IdGenero { get; set; }
        


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

        public List<FilmeModel> Listar()
        {
            List<FilmeModel> listamodel = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using(Context contexto = new Context())
            {
                FilmeRepo repo = new FilmeRepo(contexto);
                List<Filme> lista = repo.ListarTodos();
                listamodel = mapper.Map<List<FilmeModel>>(lista);
            }
            return listamodel;
        }

        public FilmeModel Selecionar(int id)
        {
            FilmeModel ? model = null;
            var mapper = new Mapper(AutoMapperConfig.RegisterMappings());

            using (Context contexto = new Context())
            {
                FilmeRepo repo = new FilmeRepo(contexto);
                Filme filme = repo.Recuperar(f => f.Id == id);
                model = mapper.Map<FilmeModel>(filme);
            }
            return model;
        }

        public void Excluir(int id)
        {
            using (Context contexto = new Context())
            {
                FilmeRepo repo = new FilmeRepo(contexto);
                Filme filme = repo.Recuperar(f => f.Id == id);
                repo.Excluir(filme);
                
                contexto.SaveChanges();
            }
        }
    }
}
