using System.ComponentModel.DataAnnotations;

namespace cargosiklink.Models.ViewModel.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "old password")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Введите новый-корректный пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "new password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Веедите правильно новый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}
