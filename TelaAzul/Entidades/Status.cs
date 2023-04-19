using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Status
    {
        public Status() 
        {
            this.Compra = new HashSet<Compra>();
        }

        public int Id { get; set; }
        public String ? Descricao { get; set; }
        public ICollection<Compra> Compra { get; set; }
    }
}
