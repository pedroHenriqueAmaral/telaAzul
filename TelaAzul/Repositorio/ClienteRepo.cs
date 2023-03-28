using Contexto;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ClienteRepo : BaseRepo<Cliente>
    {
        public ClienteRepo(Context contexto) : base(contexto) { }
    }
}
