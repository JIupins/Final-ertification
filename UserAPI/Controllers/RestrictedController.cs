using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Authorization;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestrictedController(IControllerUserSource userSource) : ControllerBase
    {
        private readonly IControllerUserSource _userSource = userSource;

        [HttpGet(template: "GetIdFromToken")]
        [Authorize(Roles = "ADMIN,USER")]
        public ActionResult GetIdFromToken()
        {
            var user = _userSource.GetUser(this);
            if (user != null)
            {
                return Ok(user.Guid);
            }
            else return StatusCode(400, "Для получения информации необходима аутентификация.");
        }
    }
}
