using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Funcionario 
    {
        public int Id { get; set; }
        public string ? Nome { get; set; }
        public DateTime Data_Nascimento { get; set; } 
        public DateTime Data_Admissao { get; set; }
        public string ? Cargo { get; set; }
        public float Salario_Fixo { get; set; }
        public String ? Email { get; set; }
        public String? Senha { get; set; }
    }
}
