public class UsuarioController : Controller
{
    private readonly DatabaseContext _context;

    public UsuarioController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var usuarios = _context.Usuarios.Include(u => u.Perfil).ToList();

        int usuariosAdmin = usuarios.Count(u => u.Perfil?.TipoPerfil == "admin");
        int usuariosUser = usuarios.Count(u => u.Perfil?.TipoPerfil == "user");
        int sessoesTotais = _context.Sessoes.Count();
        int sessoesAtivas = _context.Sessoes.Count(s => s.DataExpiracao == null);

        ViewData["UsuariosAdmin"] = usuariosAdmin;
        ViewData["UsuariosUser"] = usuariosUser;
        ViewData["SessoesTotais"] = sessoesTotais;
        ViewData["SessoesAtivas"] = sessoesAtivas;

        return View(usuarios);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Nome,Email,Senha")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Nome,Email,Senha")] Usuario usuario)
    {
        if (id != usuario.Id
    {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(usuario);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        _context.Usuarios.Remove(usuario);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    private bool UsuarioExists(int id)
    {
        return _context.Usuarios.Any(e => e.Id == id);
    }
}