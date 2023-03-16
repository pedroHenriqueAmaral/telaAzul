﻿namespace Entidades
{
    public class Filme
    {
        public int Id { get; set; }
        public string ? Titulo { get; set; }
        public string ? Titulo_original { get; set; }
        public string ? Sinopse { get; set; }
        public string ? Duracao { get; set; } // em minutos

        // ?????????????????????????????????????????????
        public string Audio { get; set; } // Dublado ou Legendado

        /* Relacionamento | Lado de "1" */
        public int IdGenero { get; set; }
        public virtual Genero ? Generos { get; set; }

        /* Relacionamnto | Lado de "Muitos" */
        public virtual ICollection <Venda> ? Vendas { get; set; } // Cliente, Ingresso
    }
}