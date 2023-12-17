using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FnssTask.Presentation.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    // GET: /<controller>/
    public async Task<IActionResult> Index()
    {
        var result = await _categoryRepository.GetAllAsync();

        return View(result);
    }


    //Get
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    //Post
    [HttpPost]
    public async Task<IActionResult> Create(Category entity)
    {
        await _categoryRepository.AddAsync(entity);

        return RedirectToAction("Index");
    }

    //Get
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var result = await _categoryRepository.GetByIdAsync(id);

        return View(result);
    }

    //Put
    [HttpPost]
    public async Task<IActionResult> Update(Category entity)
    {
        await _categoryRepository.UpdateAsync(entity);
        return RedirectToAction("Index");
    }

    //Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryRepository.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}

