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

        // Создать пост
        void CreatePost(PostViewModel viewModel);

        // Получить пост для редактирования
        PostViewModel GetPostForEdit(int id);

        // Обновить пост
        bool UpdatePost(PostViewModel viewModel);

        // Удалить пост
        bool DeletePost(int id);
    }
}