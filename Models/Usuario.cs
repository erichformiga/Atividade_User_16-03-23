using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoDeAcessos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<Perfil> Perfis { get; set; } = new List<Perfil>();
        public List<Sessao> Sessoes { get; set; } = new List<Sessao>();
    }
}

/*using System;
using System.Collections.Generic;

namespace Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set;}
        public string Email { get; set;}
        public string Senha { get; set;}
    }
}*/