using System.ComponentModel.DataAnnotations;

namespace MiniBlog.Models
{
    public class PostViewModel
    {
        [Required(ErrorMessage = "Заголовок обязателен")]
        [StringLength(100, ErrorMessage = "Заголовок не должен превышать 100 символов")]
        [MinLength(3, ErrorMessage = "Заголовок должен быть не менее 3 символов")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Текст поста обязателен")]
        [StringLength(5000, ErrorMessage = "Текст не должен превышать 5000 символов")]
        [MinLength(10, ErrorMessage = "Текст должен быть не менее 10 символов")]
        [Display(Name = "Текст поста")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(50, ErrorMessage = "Имя автора не должно превышать 50 символов")]
        [MinLength(2, ErrorMessage = "Имя автора должно быть не менее 2 символов")]
        [Display(Name = "Автор")]
        public string Author { get; set; }
    }
}