﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Ingresso
    {
        public int Id { get; set; }

        public virtual ICollection<Sala> Sala { get; set; }
        
        // Cliente
        // Data/Hora
        // Filme

    }
}
