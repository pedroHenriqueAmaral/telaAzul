using Microsoft.EntityFrameworkCore;
using Contexto;

namespace Repositorio
{
    public abstract class BaseRepo<T> where T : class
    {
        protected Context _contexto;
        private DbSet<T> _tabela;

        public BaseRepo(Context contexto)
        {
            this._contexto = contexto;
        }
    }
}