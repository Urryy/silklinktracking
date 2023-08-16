using System.ComponentModel.DataAnnotations;

namespace cargosiklink.Models.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите логин под которым будет осуществлятся вход в систему")]
        [MaxLength(20, ErrorMessage = "Логин должен быть меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Логин должен быть больше 3 символов")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите корректный пароль")]
        [MinLength(8, ErrorMessage = "Пароль должен быть больше 8 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите корректный пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введите корректный Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Введите корректный номер")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите user code")]
        public string UserCode { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string City { get; set; }

    }
}
