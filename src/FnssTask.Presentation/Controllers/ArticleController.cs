using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using FnssTask.Presentation.Models.Article;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FnssTask.Presentation.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ArticleController(IArticleRepository articleRepository, ICategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var result = await _articleRepository.GetAllAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var model = new ArticleCreateDto() { Categories = categories };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateDto model)
        {
            var entity = new Article() { Title = model.Title, Content = model.Content, CategoryId = model.CategoryId };
            await _articleRepository.AddAsync(entity);
            return RedirectToAction("Index");
        }
    }
}

