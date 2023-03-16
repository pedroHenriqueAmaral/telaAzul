using Microsoft.EntityFrameworkCore;
using Contexto;
using Entidades;
using System.Linq.Expressions;

namespace Repositorio
{
    public abstract class BaseRepo<T> where T : class
    {
        protected Context _contexto;
        private DbSet<T> _tabela;

        public BaseRepo(Context contexto)
        {
            this._contexto = contexto;
            this._tabela = this._contexto.Set<T>();
        }

        public virtual void Inserir(T entidades)
        {
            this._tabela.Add(entidades);
        }

        public virtual void Alterar(T entidades)
        {
            this._contexto.Entry(entidades).State= EntityState.Modified;
        }

        public virtual void Excluir(T entidades)
        {
            this._contexto.Entry(entidades).State = EntityState.Deleted;
            this._tabela.Remove(entidades);
        }

        public virtual T Recuperar(Expression<Func <T, bool> > expressao)
        {
            return _tabela.Where(expressao).SingleOrDefault();
        }

        public virtual List<T> Listar(Expression<Func <T, bool>> expressao)
        {
            return _tabela.Where(expressao).ToList();
        }

        public virtual List<T> ListarTodos(Expression<Func <T, bool>> expressao)
        {
            return _tabela.ToList();
        }
    }
}