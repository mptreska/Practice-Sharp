using MiniBlog.Models;
using System.Collections.Generic;

namespace MiniBlog.Services
{
    public interface IPostService
    {
        // Получить все посты
        List<Post> GetAllPosts();

        // Получить пост по Id
        Post GetPostById(int id);

        // Создать пост из ViewModel
        void CreatePost(PostViewModel viewModel);

        // Удалить пост по Id
        bool DeletePost(int id);
    }
}