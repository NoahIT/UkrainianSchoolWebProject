using System.ComponentModel.DataAnnotations;

namespace StudyPlatform.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Эл.Почта обязательна.")]
        [EmailAddress(ErrorMessage = "Неправильный формат Эл.Почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        public string Password { get; set; }
    }
}
