using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FnssTask.Presentation.Models;
using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;

namespace FnssTask.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryRepository _categoryRepository;

    public HomeController(ILogger<HomeController> logger, ICategoryRepository articleRepository)
    {
        _logger = logger;
        _categoryRepository = articleRepository;
    }

    public async Task<IActionResult> Index()
    {
        var entity = await _categoryRepository.GetByIdAsync(1);

        entity.Name = "Updated Category";

        await _categoryRepository.UpdateAsync(entity);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

