using cargosiklink.Extensions;
using cargosiklink.Models;
using cargosiklink.Models.ViewModel.Account;
using cargosiklink.Repository.Interfaces;
using cargosiklink.Service.Interfaces;
using System.Security.Claims;

namespace cargosiklink.Service
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _baseRepository; 
        public AccountService(IBaseRepository<User> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<bool> ChangePassword(string name, ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                var user = _baseRepository.GetAll().FirstOrDefault(u => u.Name == name);
                if (user == null)
                    return false;
                if(user.Password == HashPasswordHelper.HashPassword(changePasswordViewModel.NewPassword))
                    return false;
                if (!changePasswordViewModel.NewPassword.Equals(changePasswordViewModel.ConfirmNewPassword))
                    return false;

                user.Password = HashPasswordHelper.HashPassword(changePasswordViewModel.NewPassword);
                await _baseRepository.Update(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<UserInfoViewModel> GetAboutInfo(string name)
        {
            try
            {
                var user = _baseRepository.GetAll().FirstOrDefault(u => u.Name == name);
                if(user == null)
                {
                    throw new ArgumentException($"This user with login name {name} doesn't exist");
                }
                var viewModel = new UserInfoViewModel
                {
                    Id = user.Id.ToString(),
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    Phone = user.Phone,
                    UserCode = user.UserCode,
                    City = user.City,
                };
                return Task.FromResult(viewModel);
            }
            catch (Exception)
            {
                throw new ArgumentException($"Oop's something was wrong");
            }
        }

        public Task<Guid> GetUserId(string name)
        {
            var user = _baseRepository.GetAll().FirstOrDefault(u => u.Name.Equals(name));
            if(user == null)
                return Task.FromResult(Guid.NewGuid());
            
            return Task.FromResult(user.Id);
        }

        public async Task<ClaimsIdentity> Login(LoginViewModel loginViewModel)
        {
            var claims = new ClaimsIdentity();
            try
            {
                var users = _baseRepository.GetAll();
                var user = users.FirstOrDefault(u => u.Name == loginViewModel.Login);

                if (user == null)
                    return claims;
                if(user.Password != HashPasswordHelper.HashPassword(loginViewModel.Password))
                    return claims;

                return Authenticate(user);
            }
            catch (Exception)
            {
                return claims;
            }
        }

        public async Task<bool> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                var isExistUser = _baseRepository.GetAll().FirstOrDefault(u => u.Name == registerViewModel.Name);
                if (isExistUser != null)
                    return false;
                
                var user = new User(registerViewModel.Name, 
                                    HashPasswordHelper.HashPassword(registerViewModel.Password), 
                                    registerViewModel.Email, 
                                    registerViewModel.PhoneNumber,
                                    registerViewModel.UserCode, 
                                    registerViewModel.City, 
                                    Models.Enum.Roles.user);
                await _baseRepository.Create(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
