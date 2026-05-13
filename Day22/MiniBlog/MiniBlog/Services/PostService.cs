using MiniBlog.Data;
using MiniBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniBlog.Services
{
    public class PostService : IPostService
    {
        // Внедрение контекста базы данных
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        // Получить все посты
        public List<Post> GetAllPosts()
        {
            return _context.Posts
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
        }

        // Получить пост по Id
        public Post GetPostById(int id)
        {
            return _context.Posts.Find(id);
        }

        // Создать пост из ViewModel
        public void CreatePost(PostViewModel viewModel)
        {
            var post = new Post
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                Author = viewModel.Author,
                CreatedAt = DateTime.Now
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        // Получить пост для редактирования
        public PostViewModel GetPostForEdit(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return null;
            }

            return new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Author = post.Author
            };
        }

        // Обновить пост
        public bool UpdatePost(PostViewModel viewModel)
        {
            var post = _context.Posts.Find(viewModel.Id);

            if (post == null)
            {
                return false;
            }

            post.Title = viewModel.Title;
            post.Content = viewModel.Content;
            post.Author = viewModel.Author;

            _context.SaveChanges();
            return true;
        }

        // Удалить пост
        public bool DeletePost(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return false;
            }

            _context.Posts.Remove(post);
            _context.SaveChanges();
            return true;
        }
    }
}