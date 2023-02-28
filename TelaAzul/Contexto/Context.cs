using Microsoft.EntityFrameworkCore;
using Entidades;

namespace Contexto
{
    public class Context : DbContext
    {
        public Context() 
        {
            this.Database.EnsureCreated();
        }
    }
}