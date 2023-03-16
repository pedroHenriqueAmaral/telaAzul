using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Sala
    {
        public int Id { get; set; }
        public int Numero_Assentos { get; set; }

        // ingresso
        public int IdIngresso { get; set; }
        public virtual Ingresso Ingressos { get; set; }
    }
}
