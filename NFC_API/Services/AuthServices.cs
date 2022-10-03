using Microsoft.EntityFrameworkCore;
using NFC_API.Model;
using System.Security.Cryptography;

namespace NFC_API.Services
{
    public class AuthServices
    {
        int minLife = 10;
        int lenghtSalt = 10;
        private dbContext db;
        public AuthServices(dbContext db)
        {
            this.db = db;
        }
        public async Task<UserRefreshToken?> LoginInAsync(string login, string pass)
        {
            UserModelDB? user = await db.USER.FirstOrDefaultAsync(x => x.login == login);
            if (user != null && user.pass == pass)
            {
                return TokenServices.GetRefreshTokenAsync(user, minLife);
            }
            return null;
        }
        public async Task<UserRefreshToken?> RegistrInAsync(string login, string pass)
        {
            if (await db.USER.FirstOrDefaultAsync(x => x.login == login) == null)
            {

                string? salt = GetRandomSalt();
                UserModelDB? user = new UserModelDB()
                {
                    login = login,
                    pass = pass,
                    salt = salt
                };

                db.USER.Add(user);
                await db.SaveChangesAsync();

                UserRefreshToken? refreshToken = TokenServices.GetRefreshTokenAsync(user, minLife);

                return refreshToken;
            }
            else return null;
        }
        public async Task<UserRefreshToken?> RefreshInAsync(UserRefreshToken refreshToken)
        {
            UserModelDB? user = await db.USER.FirstOrDefaultAsync(x => x.login == refreshToken.Name);

            if (refreshToken is not null &&
                user is not null &&
                TokenServices.CheckToken(refreshToken.GetAccessToken(), user.salt))//проверка токена на валидность чтобы нельзя обнавлять его если валиден, может уберу
            {
                user.salt = GetRandomSalt();
                db.SaveChanges();
                return  TokenServices.GetRefreshTokenAsync(user, minLife);
            }
            else
            {
                return null;
            }
                
        }
        string GetRandomSalt()
        {
            byte[] arr = new byte[lenghtSalt];
            RandomNumberGenerator.Fill(arr);
            return Convert.ToBase64String(arr);
        }
        public  bool CheckToken(UserAccessToken Token, string salt)
        {
          
            if (TokenServices.CheckToken(Token, salt))
            {
                return true;
            }
            else return false;
        }
        public void CheckRole(UserRefreshToken? refreshToken)
        {
            throw new NotImplementedException();
        }
        public bool CheckDateTime(UserAccessToken refreshToken)
        {
            if (TokenServices.CheckDateTimeToken(refreshToken)) return true;
            else return false;
        }
    }
} 
