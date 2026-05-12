using System;
using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст поста")]
        public string Content { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime DatePosted { get; set; }
    }
}