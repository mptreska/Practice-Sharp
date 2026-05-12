using Microsoft.AspNetCore.Mvc;
using MiniBlog.Models;

namespace MiniBlog.Controllers
{
    public class PostsController : Controller
    {
        // GET: /Posts/Index
        // Список всех постов
        public IActionResult Index()
        {
            var posts = PostRepository.GetAll();
            return View(posts);
        }

        // GET: /Posts/Details/1
        // Маршрут: /Posts/Details/{id}
        public IActionResult Details(int id)
        {
            var post = PostRepository.GetById(id);

            if (post == null)
            {
                return NotFound($"Пост с Id={id} не найден");
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
        // Обработать данные из формы
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                PostRepository.Add(post);
                // После создания перенаправляем на список
                return RedirectToAction("Index");
            }

            // Если ошибки - показываем форму снова
            return View(post);
        }
    }
}