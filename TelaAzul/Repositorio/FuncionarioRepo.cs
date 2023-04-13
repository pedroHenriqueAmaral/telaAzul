using Contexto;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class FuncionarioRepo : BaseRepo<Funcionario>
    {
        public FuncionarioRepo(Context contexto) : base(contexto) { }
    }
}
