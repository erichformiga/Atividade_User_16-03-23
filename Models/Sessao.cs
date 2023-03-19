using System;
using System.Collections.Generic;

namespace Model
{
    public class Sessao
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Token { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}