using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC_API.Model;
using NFC_API.Services;

namespace NFC_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthServices auth;
        private readonly dbContext db;
        public AuthController(dbContext dbContext)
        {
            db = dbContext;
            this.auth = new AuthServices(db);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Dictionary<string,string> arguments)
        {
            UserLoginModel user = JsonConvert.DeserializeObject<UserLoginModel>(JsonConvert.SerializeObject(arguments));
            UserRefreshToken? refreshToken = await auth.LoginInAsync(user.UserName, user.Password);
            if (refreshToken != null)
            {
                return base.Content(JsonConvert.SerializeObject(refreshToken),
                    "application/json; charset=utf-8");
            }
            else return BadRequest();
        }
    }
}
