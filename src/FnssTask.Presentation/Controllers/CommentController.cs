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

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(int id)
        {
            var comments = await _commentRepository.GetAllWithArticleAsync(id);

            return View(comments);
        }

        //Get
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //Get
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            return RedirectToAction("Index");
        }

        //Get
        public async Task<IActionResult> Update(int id)
        {
            return View();
        }

        //Get
        [HttpPost]
        public async Task<IActionResult> Update(Comment comment)
        {
            return RedirectToAction("Index");
        }
    }
}

