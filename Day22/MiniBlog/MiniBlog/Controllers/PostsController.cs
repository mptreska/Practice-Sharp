using Microsoft.AspNetCore.Mvc;
using MiniBlog.Models;
using MiniBlog.Services;

namespace MiniBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: /Posts
        // Список всех постов
        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts();
            ViewBag.PostCount = posts.Count;
            return View(posts);
        }

        // GET: /Posts/Details/1
        public IActionResult Details(int id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
            {
                TempData["ErrorMessage"] = "Пост не найден";
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(viewModel);
                TempData["SuccessMessage"] = "Пост успешно опубликован";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Posts/Edit/1
        public IActionResult Edit(int id)
        {
            var viewModel = _postService.GetPostForEdit(id);

            if (viewModel == null)
            {
                TempData["ErrorMessage"] = "Пост не найден";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // POST: /Posts/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _postService.UpdatePost(viewModel);

                if (result)
                {
                    TempData["SuccessMessage"] = "Пост успешно обновлен";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Пост не найден";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // POST: /Posts/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            bool result = _postService.DeletePost(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Пост успешно удален";
            }
            else
            {
                TempData["ErrorMessage"] = "Пост не найден";
            }

            return RedirectToAction("Index");
        }
    }
}