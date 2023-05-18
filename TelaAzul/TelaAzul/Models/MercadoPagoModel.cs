﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2023_v2.Models
{
    public class MercadoPagoModel
    {
        public int IdPagamento { get; set; }
        public String ? Email { get; set; }
        public String ? Nome { get; set; }
        public String ? Cidade { get; set; }
        public String ? Estado { get; set; }
        public String ? Telefone { get; set; }
        public String ? Logradouro { get; set; }
        public String ? Numero { get; set; }
        public String ? Cep { get; set; }
        public String ? NomePlano { get; set; }
        public decimal Valor { get; set; }
    }
}
