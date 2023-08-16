using cargosiklink.Models.ViewModel.Account;
using System.Security.Claims;

namespace cargosiklink.Service.Interfaces
{
    public interface IAccountService
    {
        Task<Guid> GetUserId(string name); 

        Task<bool> Register(RegisterViewModel registerViewModel);
        Task<ClaimsIdentity> Login(LoginViewModel loginViewModel);
        Task<bool> ChangePassword(string email, ChangePasswordViewModel changePasswordViewModel);

        Task<UserInfoViewModel> GetAboutInfo(string name);
    }
}
