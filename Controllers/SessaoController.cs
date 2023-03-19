public class SessaoController : Controller
{
    private readonly DatabaseContext _context;
    public SessaoController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Email,Senha")] LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == loginViewModel.Email && u.Senha == loginViewModel.Senha);
            if (usuario == null)
            {
                TempData["Mensagem"] = "Email e/ou senha inválidos.";
                return View(loginViewModel);
            }

            var token = Guid.NewGuid().ToString();
            var sessao = new Sessao
            {
                UsuarioId = usuario.Id,
                Token = token,
                DataCriacao = DateTime.Now,
                DataExpiracao = null
            };

            _context.Add(sessao);
            _context.SaveChanges();

            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("Email", usuario.Email);
            HttpContext.Session.SetString("Perfil", usuario.Perfil?.TipoPerfil.ToString() ?? "");

            return RedirectToAction(nameof(Index));
        }

        return View(loginViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete()
    {
        var token = HttpContext.Session.GetString("Token");
        var sessao = _context.Sessoes.FirstOrDefault(s => s.Token == token);
        if (sessao != null)
        {
            _context.Remove(sessao);
            _context.SaveChanges();
        }

        HttpContext.Session.Clear();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        var usuariosLogados = _context.Sessoes.Include(s => s.Usuario).ToList();

        var model = new List<SessaoViewModel>();
        foreach (var sessao in usuariosLogados)
        {
            var viewModel = new SessaoViewModel
            {
                Usuario = sessao.Usuario,
                EstaLogado = sessao.Token == HttpContext.Session.GetString("Token")
            };

            if (viewModel.Usuario.Perfil != null)
            {
                viewModel.TipoPerfil = viewModel.Usuario.Perfil.TipoPerfil.ToString();
            }

            model.Add(viewModel);
        }

        return View(model);
    }

    public IActionResult MiddlewareUser(ActionExecutingContext context)
    {
        var token = HttpContext.Session.GetString("Token");
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        var perfil = HttpContext.Session.GetString("Perfil");

        if (string.IsNullOrEmpty(token) || usuarioId == null || perfil != "user")
        {
            context.Result = RedirectToAction(nameof(Create));
        }
    }

    public IActionResult MiddlewareAdmin(ActionExecutingContext context)
    {
        var token = HttpContext.Session.GetString("Token");
        var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
        var perfil = HttpContext.Session.GetString("Perfil");

        if (string.IsNullOrEmpty(token) || usuarioId == null || perfil != "admin")
        {
            context.Result = RedirectToAction(nameof(Create));
        }
    }
}