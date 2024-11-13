using System.ComponentModel.DataAnnotations;

namespace StudyPlatform.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Эл.Почта обязательна.")]
        [EmailAddress(ErrorMessage = "Неправильный формат Эл.Почты.")]
        public string Email { get; set; }

        public string GradeClass { get; set; }
        public int SubscriptionId { get; set; }
        public string SubjectsId { get; set; }
    }
}
