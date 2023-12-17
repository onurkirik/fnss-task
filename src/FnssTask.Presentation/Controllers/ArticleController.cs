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
            var articles = await _articleRepository.GetAllAsync();

            foreach (var article in articles)
            {
                var category = await _categoryRepository.GetByIdAsync(article.CategoryId);

                article.Category = category;
            }


            return View(articles);
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var article = await _articleRepository.GetByIdAsync(id);

            var category = await _categoryRepository.GetByIdAsync(article.CategoryId);

            var categories = await _categoryRepository.GetAllAsync();

            article.Category = category;

            var model = new ArticleUpdateDto()
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Category = article.Category,
                Categories = categories,
                CategoryId = category.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateDto entity)
        {
            var newArticle = new Article() { Id = entity.Id, CategoryId = entity.CategoryId, Title = entity.Title, Content = entity.Content };

            await _articleRepository.UpdateAsync(newArticle);

            return RedirectToAction("Index");
        }

        //Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleRepository.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

