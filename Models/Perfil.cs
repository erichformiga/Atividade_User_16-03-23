using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoDeAcessos.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Tipo { get; set; }

        public Usuario Usuario { get; set; }
    }
}


/*using System;
using System.Collections.Generic;

namespace Model
{
    public class Perfil
    {
        public int Id { get; set;}
        public int UsuarioId { get; set; }
        public string PerfilNome { get; set;}
    }
}*/