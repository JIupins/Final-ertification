using Microsoft.AspNetCore.Mvc;
using UserService.Models.DTO;

namespace UserService.Authorization
{
    public interface IControllerUserSource
    {
        public UserDto? GetUser(ControllerBase controller);
    }
}
