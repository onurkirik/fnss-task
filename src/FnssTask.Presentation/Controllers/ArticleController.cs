using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnssTask.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FnssTask.Presentation.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var result = await _articleRepository.GetAllAsync();

            return View(result);
        }
    }
}

