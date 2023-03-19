public class PerfilController : Controller
{
    private readonly DatabaseContext _context;
    public PerfilController(DatabaseContext context)
    {
        _context = context;
    }

    public IActionResult Create(int? id)
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

        var perfil = _context.Perfis.FirstOrDefault(p => p.UsuarioId == id);
        if (perfil != null)
        {
            TempData["Mensagem"] = "Usuário já possui um perfil.";
            return RedirectToAction("Index", "Usuario");
        }

        ViewData["Usuario"] = usuario;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("UsuarioId,TipoPerfil")] Perfil perfil)
    {
        if (ModelState.IsValid)
        {
            _context.Add(perfil);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index), "Usuario");
        }

        ViewData["Usuario"] = _context.Usuarios.Find(perfil.UsuarioId);
        return View(perfil);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = _context.Perfis.Find(id);
        if (perfil == null)
        {
            return NotFound();
        }

        ViewData["Usuario"] = _context.Usuarios.Find(perfil.UsuarioId);
        return View(perfil);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,UsuarioId,TipoPerfil")] Perfil perfil)
    {
        if (id != perfil.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(perfil);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(perfil.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), "Usuario");
        }

        ViewData["Usuario"] = _context.Usuarios.Find(perfil.UsuarioId);
        return View(perfil);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var perfil = _context.Perfis.Include(p => p.Usuario).FirstOrDefault(p => p.Id == id);
        if (perfil == null)
        {
            return NotFound();
        }

        return View(perfil);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var perfil = _context.Perfis.Find(id);
        _context.Perfis.Remove(perfil);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index), "Usuario");
    }

    private bool PerfilExists(int id)
    {
        return _context.Perfis.Any(e => e.Id == id);
    }
}