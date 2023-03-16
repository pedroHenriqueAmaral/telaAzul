using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ingresso
    {
        public int Id { get; set; }
        public string ? Tipo { get; set; } // Normal ou 3D  
        public virtual ICollection<Sala> ? Sala { get; set; }
        public DateTime Data_Hora_Sessao { get; set; }

        // valor do ingresso
        public float Valor { get; set; }

        // Vendas = Cliente + Filme + Ingresso
        public virtual ICollection<Venda> Vendas { get; set; }



    }
}
