using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FnssTask.Presentation.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IArticleRepository _articleRepository;

        public CommentController(ICommentRepository commentRepository, IArticleRepository articleRepository)
        {
            _commentRepository = commentRepository;
            _articleRepository = articleRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {
            var comments = await _commentRepository.GetAllWithArticleAsync(id);
            ViewBag.ArticleId = id;

            return View(comments);
        }

        //Get
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.articleId = id;

            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            await _commentRepository.AddAsync(comment);

            return RedirectToAction("Index", new { id = comment.ArticleId });
        }

        //Get
        public async Task<IActionResult> Update(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            return View(comment);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Update(Comment comment)
        {
            await _commentRepository.UpdateAsync(comment);

            return RedirectToAction("Index", new { id = comment.ArticleId });
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentRepository.DeleteAsync(id);

            return RedirectToAction("Index", "Article");
        }
    }
}

