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
        private AuthServices AUTH;
        private readonly dbContext db;
        public AuthController(dbContext dbContext)
        {
            db = dbContext;
            this.AUTH = new AuthServices(db);
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Dictionary<string, string> arguments)
        {
            UserLoginModel? user = JsonConvert.DeserializeObject<UserLoginModel>(JsonConvert.SerializeObject(arguments));
            UserRefreshToken? refreshToken = await AUTH.LoginInAsync(user.UserName, user.Password);
            if (refreshToken != null)
            {
                return base.Content(JsonConvert.SerializeObject(refreshToken),
                    "application/json; charset=utf-8");
            }
            else return BadRequest();
        }
        [HttpPost("Registr")]
        public async Task<ActionResult> Registr([FromBody] Dictionary<string, string> arguments)
        {
            UserLoginModel? user = JsonConvert.DeserializeObject<UserLoginModel>(JsonConvert.SerializeObject(arguments));
            UserRefreshToken? refreshToken = await AUTH.RegistrInAsync(user.UserName, user.Password);
            if (refreshToken != null)
            {
                return base.Content(JsonConvert.SerializeObject(refreshToken),
                    "application/json; charset=utf-8");
            }
            else return BadRequest();
        }
        [HttpPost("Refresh")]
        public async Task<ActionResult> Refresh([FromBody] Dictionary<string, string> arguments)
        {
            UserRefreshToken? refreshToken = JsonConvert.DeserializeObject<UserRefreshToken>(JsonConvert.SerializeObject(arguments));
            if (refreshToken is not null)
            {
                refreshToken = await AUTH.RefreshInAsync(refreshToken);
                if (refreshToken is not null)
                    return base.Content(JsonConvert.SerializeObject(refreshToken),
                        "application/json; charset=utf-8");
                else return BadRequest();
            }
            else return BadRequest();
        }
    }
}
