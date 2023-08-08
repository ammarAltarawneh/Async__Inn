using Async__Inn.Models.DTO;
using Async__Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async__Inn.Models.Services
{
    public class IdentityUserService : IUser
    {
        private UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            userManager = manager;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);

            bool validPassword = await userManager.CheckPasswordAsync(user, password);

            if (validPassword)
            {
                return new UserDto { Id = user.Id, UserName = user.UserName };
            }

            return null;
        }

        public async Task<UserDto> Register(RegisterUserDto registerUser, ModelStateDictionary modelState)
        {
            //throw new NotImplementedException();

            var user = new ApplicationUser()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
            };

            var result = await userManager.CreateAsync(user, registerUser.Password);


            if (result.Succeeded)
            {
                return new UserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUser.Password) :
                               error.Code.Contains("Email") ? nameof(registerUser.Email) :
                               error.Code.Contains("Username") ? nameof(registerUser.UserName) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
