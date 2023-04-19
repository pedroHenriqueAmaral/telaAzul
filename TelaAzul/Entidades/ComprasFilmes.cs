using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ComprasFilmes
    {
        public int Id { get; set; }
        public int Quantidade { get; set; } 
        public Decimal Valor { get; set; }
        
        public int CompraId { get; set; }
        public virtual Compra ? Compra { get; set; }
        
        public int FilmeId { get; set; }
        public virtual Filme ? Filme { get; set; }
    }
}
