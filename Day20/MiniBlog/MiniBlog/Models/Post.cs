using System;
using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите заголовок")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите содержимое")]
        [Display(Name = "Содержимое")]
        public string Content { get; set; }

        [Display(Name = "Дата публикации")]
        public DateTime DatePosted { get; set; }
    }
}