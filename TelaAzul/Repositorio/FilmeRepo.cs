using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Contexto;

namespace Repositorio
{
    public class FilmeRepo : BaseRepo<Filme>
    {
        public FilmeRepo(Context contexto) : base(contexto) {
        
        

        }
        
    }
}
