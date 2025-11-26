using AutoMapper;
using BlackCoffe.UI.DTOs;
using BlackCoffe.UI.Services.Interfaces;
using BlackCoffe.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackCoffe.UI.Controllers;

[Authorize(Roles = "Administrador")]
public class CategoriasController : Controller
{
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(
        ICategoriaService categoriaService,
        IMapper mapper,
        ILogger<CategoriasController> logger)
    {
        _categoriaService = categoriaService;
        _mapper = mapper;
        _logger = logger;
    }

    // GET: Categorias
    public async Task<IActionResult> Index()
    {
        try
        {
            var categorias = await _categoriaService.ObterTodasAsync();
            var categoriasVM = _mapper.Map<List<CategoriaVM>>(categorias);
            return View(categoriasVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar categorias");
            TempData["Erro"] = "Erro ao carregar categorias. Tente novamente.";
            return View(new List<CategoriaVM>());
        }
    }

    // GET: Categorias/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            if (categoria == null)
            {
                TempData["Erro"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaVM>(categoria);
            return View(categoriaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar categoria {Id}", id);
            TempData["Erro"] = "Erro ao carregar categoria.";
            return RedirectToAction(nameof(Index));
        }
    }

    // GET: Categorias/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categorias/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoriaVM categoriaVM)
    {
        if (!ModelState.IsValid)
            return View(categoriaVM);

        try
        {
            // Mapeamento manual para evitar problemas com IFormFile
            var categoriaDto = new CategoriaDto
            {
                Nome = categoriaVM.Nome,
                Cor = categoriaVM.Cor
                // Foto será tratada separadamente pelo serviço
            };

            bool success;

            if (categoriaVM.Foto != null && categoriaVM.Foto.Length > 0)
            {
                success = await _categoriaService.CriarComFotoAsync(categoriaDto, categoriaVM.Foto);
            }
            else
            {
                success = await _categoriaService.CriarAsync(categoriaDto);
            }

            if (success)
            {
                TempData["Sucesso"] = "Categoria criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao criar categoria.");
            return View(categoriaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar categoria");
            ModelState.AddModelError("", "Erro interno ao criar categoria.");
            return View(categoriaVM);
        }
    }

    // GET: Categorias/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            if (categoria == null)
            {
                TempData["Erro"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaVM>(categoria);
            return View(categoriaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar categoria para edição {Id}", id);
            TempData["Erro"] = "Erro ao carregar categoria.";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Categorias/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoriaVM categoriaVM)
    {
        if (id != categoriaVM.Id)
        {
            TempData["Erro"] = "Categoria inválida.";
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
            return View(categoriaVM);

        try
        {
            // Mapeamento manual
            var categoriaDto = new CategoriaDto
            {
                Id = categoriaVM.Id,
                Nome = categoriaVM.Nome,
                Cor = categoriaVM.Cor
                // Foto será tratada separadamente pelo serviço
            };

            bool success;

            if (categoriaVM.Foto != null && categoriaVM.Foto.Length > 0)
            {
                success = await _categoriaService.AtualizarComFotoAsync(id, categoriaDto, categoriaVM.Foto);
            }
            else
            {
                success = await _categoriaService.AtualizarAsync(id, categoriaDto);
            }

            if (success)
            {
                TempData["Sucesso"] = "Categoria atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao atualizar categoria.");
            return View(categoriaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar categoria {Id}", id);
            ModelState.AddModelError("", "Erro interno ao atualizar categoria.");
            return View(categoriaVM);
        }
    }

    // GET: Categorias/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var categoria = await _categoriaService.ObterPorIdAsync(id);
            if (categoria == null)
            {
                TempData["Erro"] = "Categoria não encontrada.";
                return RedirectToAction(nameof(Index));
            }

            var categoriaVM = _mapper.Map<CategoriaVM>(categoria);
            return View(categoriaVM);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar categoria para exclusão {Id}", id);
            TempData["Erro"] = "Erro ao carregar categoria.";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Categorias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var success = await _categoriaService.ExcluirAsync(id);

            if (success)
            {
                TempData["Sucesso"] = "Categoria excluída com sucesso!";
            }
            else
            {
                TempData["Erro"] = "Erro ao excluir categoria.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir categoria {Id}", id);
            TempData["Erro"] = "Erro interno ao excluir categoria.";
        }

        return RedirectToAction(nameof(Index));
    }
}