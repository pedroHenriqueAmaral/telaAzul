using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Compra
    {
        public Compra()
        {
            this.ComprasFilmes = new HashSet<ComprasFilmes>();
        }

        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public Decimal Valor { get; set; }

        public int StatusId { get; set; }
        public virtual Status ? Status { get; set; }

        public virtual ICollection<ComprasFilmes> ComprasFilmes { get; set; }

        /*
        public virtual ICollection <Ingresso> ? Ingresso { get; set; }
        public virtual ICollection <Cliente> ? Cliente { get; set; }
        public virtual ICollection <Filme> ? Filme { get; set; }
        */  
    }
}
