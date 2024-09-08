using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Models;
using UserService.Models.DTO;

namespace UserService.Authorization
{
    public class ClaimsUserSource : IControllerUserSource
    {
        public UserDto? GetUser(ControllerBase controller)
        {
            var identity = controller.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserDto()
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    RoleId = (RoleId)Enum.Parse(typeof(RoleId),
                        userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value),
                    Guid = Guid.Parse(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber)?.Value)
                };
            }
            else return null;
        }
    }
}
