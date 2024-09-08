using UserService.Models;

namespace UserService.Authentication
{
    public interface ITokenSource
    {
        public string CreateToken(string email, RoleId role, Guid guid);
    }
}
