using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string ? Nome { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string ? Tipo_Cliente { get; set; } // Estudante, Criança, Comum
        public string ? Email { get; set; } // usado para login (possibilidade para newsletter)
        public string ? Senha { get; set; }
        
        
        // Cliente > Vendas < Ingresso
        // public virtual ICollection <Venda> ? Vendas { get; set; }
    }
}
