namespace Entidades
{
    public class Filme
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        /* ... */



        /* Relacionamento | Lado de "1" */
        public int IdCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }
        
    }
}