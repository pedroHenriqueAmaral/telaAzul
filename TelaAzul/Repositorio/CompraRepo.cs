using Contexto;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class CompraRepo : BaseRepo<Compra>
    {
        public CompraRepo(Context contexto) : base(contexto)
        {
        }
    }
}
