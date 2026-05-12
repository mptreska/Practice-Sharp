using MiniBlog.Models;
using System;
using System.Collections.Generic;

namespace MiniBlog.Services
{
    public class PostService : IPostService
    {
        // Хранилище постов (в памяти)
        private static List<Post> _posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "Первый пост",
                Content = "Это содержимое первого поста в нашем блоге",
                Author = "Иван Иванов",
                DatePosted = DateTime.Now.AddDays(-5)
            },
            new Post
            {
                Id = 2,
                Title = "Второй пост",
                Content = "Это содержимое второго поста в нашем блоге",
                Author = "Мария Петрова",
                DatePosted = DateTime.Now.AddDays(-2)
            },
            new Post
            {
                Id = 3,
                Title = "Третий пост",
                Content = "Это содержимое третьего поста в нашем блоге",
                Author = "Алексей Сидоров",
                DatePosted = DateTime.Now
            }
        };

        private static int _nextId = 4;

        // Получить все посты
        public List<Post> GetAllPosts()
        {
            return _posts;
        }

        // Получить пост по Id
        public Post GetPostById(int id)
        {
            return _posts.Find(p => p.Id == id);
        }

        // Создать пост из ViewModel
        public void CreatePost(PostViewModel viewModel)
        {
            var post = new Post
            {
                Id = _nextId++,
                Title = viewModel.Title,
                Content = viewModel.Content,
                Author = viewModel.Author,
                DatePosted = DateTime.Now
            };

            _posts.Add(post);
        }

        // Удалить пост по Id
        public bool DeletePost(int id)
        {
            var post = _posts.Find(p => p.Id == id);

            if (post == null)
            {
                return false; // Пост не найден
            }

            _posts.Remove(post);
            return true; // Успешно удалён
        }
    }
}