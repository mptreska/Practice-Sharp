using Microsoft.AspNetCore.Mvc;
using MiniBlog.Models;
using MiniBlog.Services;

namespace MiniBlog.Controllers
{
    public class PostsController : Controller
    {
        // Внедрение зависимости через конструктор (DI)
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: /Posts/Index
        // Список всех постов
        public IActionResult Index()
        {
            var posts = _postService.GetAllPosts();

            // ViewBag - передаём количество постов в представление
            ViewBag.PostCount = posts.Count;

            return View(posts);
        }

        // GET: /Posts/Details/1
        public IActionResult Details(int id)
        {
            var post = _postService.GetPostById(id);

            if (post == null)
            {
                return NotFound($"Пост с Id = {id} не найден");
            }

            return View(post);
        }

        // GET: /Posts/Create
        // Показать форму создания
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Posts/Create
        // Сохранить данные из формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Создаём пост через сервис
                _postService.CreatePost(viewModel);

                // TempData - сообщение которое покажем после редиректа
                TempData["SuccessMessage"] = "Пост опубликован!";

                return RedirectToAction("Index");
            }

            // Если ошибки валидации - показываем форму снова
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
                // TempData - сообщение об успешном удалении
                TempData["SuccessMessage"] = "Пост удалён!";
            }
            else
            {
                TempData["ErrorMessage"] = "Пост не найден!";
            }

            return RedirectToAction("Index");
        }
    }
}