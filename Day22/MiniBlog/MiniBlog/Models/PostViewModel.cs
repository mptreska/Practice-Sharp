using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, ErrorMessage = "Не более 100 символов")]
        [MinLength(3, ErrorMessage = "Не менее 3 символов")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Текст поста обязателен")]
        [StringLength(5000, ErrorMessage = "Не более 5000 символов")]
        [MinLength(10, ErrorMessage = "Не менее 10 символов")]
        [Display(Name = "Текст поста")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        [MinLength(2, ErrorMessage = "Не менее 2 символов")]
        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}