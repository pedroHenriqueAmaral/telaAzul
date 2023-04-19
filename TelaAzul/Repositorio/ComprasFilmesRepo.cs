using Contexto;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ComprasFilmesRepo : BaseRepo<ComprasFilmes>
    {
        public ComprasFilmesRepo(Context contexto) : base(contexto)
        {
        }
    }
}
