﻿namespace Entidades
{
    public class Filme
    {
        public int Id { get; set; }
        public string ? Titulo { get; set; }
        //public string ? Titulo_original { get; set; } // no idioma orignal
        public string ? Sinopse { get; set; }
        public string ? Duracao { get; set; } // em minutos
        public string ? Audio { get; set; } // dublado ou legendado
        
        public int ? GeneroId { get; set; }
        public virtual Genero ? Genero { get; set; }

        public virtual ICollection<Studio> ? Studios { get; set; }
        // public virtual ICollection<FilmeAtor> FilmeAtores { get; set; }
    }
}