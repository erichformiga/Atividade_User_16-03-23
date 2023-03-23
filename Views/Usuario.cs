using GerenciamentoDeAcessos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GerenciamentoDeAcessos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _contexto;

        public UsuarioController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var usuarios = _contexto.Usuarios.ToList();
            return View(usuarios);
        }

        public IActionResult Detalhes(int id)
        {
            var usuario = _contexto.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }



/*namespace View
{
    public class Usuario
    {
        public static void CriarUsuario()
        {
           Console.WriteLine("Criar usuário:");
           Console.WriteLine("Nome: ");
           string Nome = Console.ReadLine();
           Console.WriteLine("E-mail: ");
           string Email = Console.ReadLine();
           Console.WriteLine("Senha: ");
           string Senha = Console.ReadLine();

           Usuario usuario = new Usuario
           {
                Id = nextUsuarioId++,
                Nome = nome,
                Email = Email,
                Senha = senha
           };
           _usuario.Add(usuario);

           Console.WriteLine("Usuário Criado com Sucesso.");
        }
    }

    private void AlterarUsuario()
    {
        Console.WriteLine("Alterar Usuário: ");
        Console.WriteLine("Informe o ID. do Usuário: ");
        int Id = int.Parse(Console.ReadLine());
        
        Usuario usuario = usuario.FristOrDefaut(u => u,Id == Id);
        if (Usuario == null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }
        Console.WriteLine("Nome (atual: {0}:) ", usuario.Nome);
        string nome = Console.ReadLine();
        Console.WriteLine("E-mail (atual: {0}): ", usuario.Email);
        string email = Console.WriteLine();
        Console.WriteLine("Senha (atual: {0}): ", usuario.Senha);

        usuario.Nome = nome;
        usuario.Email = email;
        usuario.Senha = senha;

        Console.WriteLine("Usuário alterado com sucesso!");
    }

    private void ExcluirUsuario()
    {    
        {
            Console.WriteLine("Excluir usuário:");
            Console.WriteLine("ID. di usuário: ");
            Int128 id = int.Parse(Console.ReadLine());

            Usuario usuario = usuario.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            _usuario.Remove(Usuario);
            Console.WriteLine("Usuário excluído com sucesso.");
        }
    }

    private void ListarUsuarios()
    {
        Console.WriteLine("Usuários:");
        Console.WriteLine("Id  Nome             Email               Perfil");
        Console.WriteLine("--  ----             -----               ------");
        foreach (var usuario in _usuarios)
        {
            Console.Write("{0,-4}", usuario.Id);
            Console.Write("{0,-16}", usuario.Nome);
            Console.Write("{0,-20}", usuario.Email);
            Console.Write("{0,-6}", usuario.Perfil != null ? usuario.Perfil.Nome : "");
            Console.WriteLine();
        }
    }
}*/