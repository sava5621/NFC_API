using Microsoft.EntityFrameworkCore;
using NFC_API.Model;

namespace NFC_API.Services
{
    public class AuthServices
    {
        int minLife = 10;
        private dbContext db;
        public AuthServices(dbContext db)
        {
            this.db = db;
        }
        public async Task<UserRefreshToken?> LoginInAsync(string login, string pass)
        {
            UserModelDB? user = await db.USER.FirstOrDefaultAsync(x => x.login == login);
            if (user != null && user.pass==pass)
            {
               return TokenServices.GetRefreshTokenAsync(user, minLife);
            }
            return null;
        }
    }
}
