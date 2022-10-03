using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NFC_API.Model;
using NFC_API.Services;

namespace NFC_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private AuthServices AUTH;
        private DataServices DATA;
        private readonly dbContext db;
        public ItemController(dbContext dbContext)
        {
            db = dbContext;
            this.AUTH = new(db);
            this.DATA = new(db);
        }
        [HttpGet()]
        public async Task<ActionResult> GetAsync([FromBody] Dictionary<string, string> arguments)
        {
            // AUTH.CheckRole(refreshToken); // TODO: когда будут роли
            UserAccessToken? accessToken = JsonConvert.DeserializeObject<UserAccessToken>(JsonConvert.SerializeObject(arguments));
            UserModelDB? user = await db.USER.FirstOrDefaultAsync(x => x.login == accessToken.Name);
            if (accessToken is not null
                && user is not null
                && AUTH.CheckToken(accessToken, user.salt)
                && AUTH.CheckDateTime(accessToken))
                return base.Content(JsonConvert.SerializeObject( await DATA.GetData()),
                        "application/json; charset=utf-8");

            else return BadRequest();
        }
    }
}
