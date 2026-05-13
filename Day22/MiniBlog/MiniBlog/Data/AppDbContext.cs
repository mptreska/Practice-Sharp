using Microsoft.EntityFrameworkCore;
using MiniBlog.Models;

namespace MiniBlog.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Таблица постов в базе данных
        public DbSet<Post> Posts { get; set; }

        // Начальные данные в базе
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Title = "Первый пост",
                    Content = "Это содержимое первого поста в нашем блоге",
                    Author = "Иван Иванов",
                    CreatedAt = new DateTime(2026, 1, 1)
                },
                new Post
                {
                    Id = 2,
                    Title = "Второй пост",
                    Content = "Это содержимое второго поста в нашем блоге",
                    Author = "Мария Петрова",
                    CreatedAt = new DateTime(2026, 1, 5)
                },
                new Post
                {
                    Id = 3,
                    Title = "Третий пост",
                    Content = "Это содержимое третьего поста в нашем блоге",
                    Author = "Алексей Сидоров",
                    CreatedAt = new DateTime(2026, 1, 10)
                }
            );
        }
    }
}