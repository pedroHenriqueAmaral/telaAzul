using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Studio
    {
        public int Id;
        public string ? Nome;
        public int ? FilmeId { get; set; }
        public virtual Filme ? Filme { get; set; }
    }
}
