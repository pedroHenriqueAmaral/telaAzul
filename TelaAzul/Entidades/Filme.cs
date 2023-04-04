namespace Entidades
{
    public class Filme
    {
        public int Id { get; set; }
        public string ? Titulo { get; set; }
        public string ? Imagem { get; set; }
        public string ? Sinopse { get; set; }
        public string ? Duracao { get; set; } // em minutos
        public string ? Audio { get; set; } // dublado ou legendado

        /* Relacionamentos */
        
        // Lado de Um
        public int ? GeneroId { get; set; }
        public virtual Genero ? Genero { get; set; }

        // Lado Muitos
        public virtual ICollection<Studio> ? Studios { get; set; }

        // public virtual ICollection<FilmeAtor> FilmeAtores { get; set; }
    }
}