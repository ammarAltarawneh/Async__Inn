using Async__Inn.Models.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async__Inn.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDto> Register(RegisterUserDto registerUser, ModelStateDictionary modelState);

        public Task<UserDto> Authenticate(string username, string password);
    }
}
