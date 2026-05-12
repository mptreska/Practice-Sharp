using System;
using System.Collections.Generic;

namespace MiniBlog.Models
{
    public static class PostRepository
    {
        // Статический список - хранит посты пока работает приложение
        private static List<Post> _posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "Первый пост",
                Content = "Это содержимое первого поста",
                DatePosted = DateTime.Now.AddDays(-5)
            },
            new Post
            {
                Id = 2,
                Title = "Второй пост",
                Content = "Это содержимое второго поста",
                DatePosted = DateTime.Now.AddDays(-2)
            },
            new Post
            {
                Id = 3,
                Title = "Третий пост",
                Content = "Это содержимое третьего поста",
                DatePosted = DateTime.Now
            }
        };

        // Счётчик для автоматического Id
        private static int _nextId = 4;

        // Получить все посты
        public static List<Post> GetAll()
        {
            return _posts;
        }

        // Получить пост по Id
        public static Post GetById(int id)
        {
            return _posts.Find(p => p.Id == id);
        }

        // Добавить новый пост
        public static void Add(Post post)
        {
            post.Id = _nextId++;
            post.DatePosted = DateTime.Now;
            _posts.Add(post);
        }
    }
}