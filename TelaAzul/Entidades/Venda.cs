using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Venda
    {
        public int Id { get; set; }
        public virtual ICollection <Ingresso> ? Ingresso { get; set; }
        public virtual ICollection <Cliente> ? Cliente { get; set; }
        public virtual ICollection <Filme> ? Filme { get; set; }

    }
}
