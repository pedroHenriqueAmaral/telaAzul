namespace Entidades
{
    public class Filme
    {
        public Filme()
        {
            this.ComprasFilmes = new HashSet<ComprasFilmes>();
        }

        public int Id { get; set; }
        public string ? Titulo { get; set; }
        public string ? Imagem { get; set; }
        public string ? Sinopse { get; set; }
        public string ? Duracao { get; set; } // em minutos
        public string ? Audio { get; set; } // dublado ou legendado
        public Decimal Valor { get; set; }
        
        /* Relacionamento | Lado de Um */
        public int GeneroId { get; set; }
        public virtual Genero ? Genero { get; set; }

        /* Relacionamento Muitos pra Muitos com Compra */
        public virtual ICollection<ComprasFilmes> ComprasFilmes { get; set; }
    }
}