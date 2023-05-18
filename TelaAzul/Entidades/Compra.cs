using Entidades;
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
        public int IdStatus { get; set; }
        public virtual Status Status { get; set; }
        public String IdPreferencia { get; set; }
        public String Url { get; set; }
        public virtual ICollection<ComprasFilmes> ComprasFilmes { get; set; }
    }
}
