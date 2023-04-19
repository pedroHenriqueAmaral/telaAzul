using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Genero
    {
        public Genero() 
        {
            this.Filme = new HashSet<Filme>();
        }

        public int Id { get; set; }
        public string ? Nome { get; set; } // Terror, Ação, Aventura...

        /* Relacionamento | Lado de Muitos */
        public virtual ICollection<Filme> ? Filme { get; set; }
    }
}
